using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SplitBackDotnet.Data;
using SplitBackDotnet.Dtos;
using SplitBackDotnet.Helper;
using SplitBackDotnet.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SplitBackDotnet.Endpoints;

public static class AuthenticationEndpoints {
  public static void MapAuthenticationEndpoints(this IEndpointRouteBuilder app) {

    app.MapPost("/auth/verify-token", async (HttpResponse response, IConfiguration config, DataContext context, [FromBody] TokenBody tokenBody) => {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.UTF8.GetBytes(config["Jwt:Key"]);

      try {
        tokenHandler.ValidateToken(tokenBody.Token, new TokenValidationParameters {
          ValidateIssuerSigningKey = false,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = false,
          ValidateAudience = false,
          // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
          ClockSkew = TimeSpan.Zero
        }, out SecurityToken validatedToken);

        var validatedJwtToken = (JwtSecurityToken)validatedToken;
        var type = validatedJwtToken.Payload.Claims.First(claim => claim.Type == "type").Value;
        var unique = validatedJwtToken.Payload.Claims.First(claim => claim.Type == "unique").Value;
        if(unique == null) return Results.BadRequest("No unique cookie");

        var sessionFound = await context.Sessions.FirstOrDefaultAsync(session => session.Unique == unique);
        if(sessionFound != null) return Results.BadRequest("Verification link already used");

        var newRefreshToken = Guid.NewGuid().ToString();

        if(type == "sign-up") {
          var email = validatedJwtToken.Payload.Claims.First(claim => claim.Type == "email").Value;
          var nickanme = validatedJwtToken.Payload.Claims.First(claim => claim.Type == "nickname").Value;

          var newUser = new User {
            Email = email,
            Nickname = nickanme,
          };
          context.Add(newUser);

          var newSession = new Session {
            RefreshToken = newRefreshToken,
            User = newUser,
            Unique = unique
          };
          context.Add(newSession);
          await context.SaveChangesAsync();

          return Results.Ok(new { type = "sign-up" });

        } else if(type == "sign-in") {
          var email = validatedJwtToken.Payload.Claims.First(claim => claim.Type == "email").Value;
          var userFound = context.Users.FirstOrDefault(user => user.Email == email);
          if(userFound == null) return Results.NotFound("User does not exist");

          var newSession = new Session {
            RefreshToken = newRefreshToken,
            User = userFound,
            Unique = unique
          };
          context.Add(newSession);
          await context.SaveChangesAsync();

          return Results.Ok(new { type = "sign-in" });
        } else return Results.BadRequest("Invalid token");

      } catch(Exception e) {
        Console.WriteLine(e.Message);
        return Results.Ok("JWT validation error");
      }
    });

    app.MapPost("/auth/sign-in", async (IConfiguration config, HttpRequest request, HttpResponse response, DataContext context) => {
      var unique = request.Cookies["unique"];
      if(unique == null) return Results.Unauthorized();

      var sessionFound = await context.Sessions.Include(session => session.User).FirstOrDefaultAsync(session => session.Unique == unique);
      if(sessionFound == null) return Results.Unauthorized();

      var userFound = await context.Users.FirstOrDefaultAsync(user => user == sessionFound.User);
      if(userFound == null) return Results.Unauthorized();

      response.Cookies.Delete("unique", new CookieOptions {
        SameSite = SameSiteMode.Lax,
        HttpOnly = true,
        Path = "/auth/sign-in",
        Expires = DateTime.UtcNow.AddDays(30),
        MaxAge = TimeSpan.FromDays(30)
      });

      response.Cookies.Append("refreshToken", sessionFound.RefreshToken, new CookieOptions {
        SameSite = SameSiteMode.Lax,
        HttpOnly = true,
        Path = "/auth/refreshtoken",
        Expires = DateTime.UtcNow.AddDays(30),
        MaxAge = TimeSpan.FromDays(30)
      });

      var secureKey = Encoding.UTF8.GetBytes(config["Jwt:Key"]);
      var securityKey = new SymmetricSecurityKey(secureKey);
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
      var jwtTokenHandler = new JwtSecurityTokenHandler();

      var tokenDescriptor = new SecurityTokenDescriptor {
        Subject = new ClaimsIdentity(new[] {
          new Claim("userId", sessionFound.User.Id.ToString())
        }),
        Expires = DateTime.Now.AddMinutes(1),
        Audience = config["Jwt:Audience"],
        Issuer = config["Jwt:Issuer"],
        SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512)
      };

      var token = jwtTokenHandler.CreateToken(tokenDescriptor);

      return Results.Ok(new {
        accessToken = jwtTokenHandler.WriteToken(token),
        sessionData = new {
          id = sessionFound.Id,
          userId = userFound.Id,
          userEmail = userFound.Email,
          userNickname = userFound.Nickname
        }
      });
    });

    app.MapPost("/auth/request-sign-up", (HttpResponse response, IMapper mapper, IConfiguration config, DataContext context, UserCreateDto userCreateDto) => {

      if(context.Users.Any(_user => _user.Email == userCreateDto.Email)) {
        return Results.Ok("User already exists!");
      }

      var unique = Guid.NewGuid().ToString();

      var secureKey = Encoding.UTF8.GetBytes(config["Jwt:Key"]);
      var securityKey = new SymmetricSecurityKey(secureKey);
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
      var jwtTokenHandler = new JwtSecurityTokenHandler();

      var tokenDescriptor = new SecurityTokenDescriptor {
        Subject = new ClaimsIdentity(new[] {
          new Claim("type", "sign-up"),
          new Claim("email", userCreateDto.Email.ToString()),
          new Claim("nickname", userCreateDto.Nickname.ToString()),
          new Claim("unique", unique),
        }),
        Expires = DateTime.Now.AddMinutes(1),
        Audience = config["Jwt:Audience"],
        Issuer = config["Jwt:Issuer"],
        SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512)
      };

      var token = jwtTokenHandler.CreateToken(tokenDescriptor);
      var jwtToken = jwtTokenHandler.WriteToken(token);

      response.Cookies.Append("unique", unique, new CookieOptions {
        SameSite = SameSiteMode.Lax,
        HttpOnly = true,
        Path = "/auth/sign-in",
        Expires = DateTime.UtcNow.AddDays(30),
        MaxAge = TimeSpan.FromDays(30)
      });
      Console.WriteLine($"Sign up verification link with to {userCreateDto.Email}. {jwtToken}");
      return Results.Ok($"Sign up verification link with to {userCreateDto.Email}. {jwtToken}");
    });

    app.MapPost("/auth/request-sign-in", async (EmailBody emailBody, HttpResponse response, IConfiguration config, DataContext context) => {
      var userFound = await context.Users.FirstOrDefaultAsync(user => user.Email == emailBody.Email);
      if(userFound == null) return Results.Unauthorized();

      var unique = Guid.NewGuid().ToString();

      var secureKey = Encoding.UTF8.GetBytes(config["Jwt:Key"]);
      var securityKey = new SymmetricSecurityKey(secureKey);
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
      var jwtTokenHandler = new JwtSecurityTokenHandler();

      var tokenDescriptor = new SecurityTokenDescriptor {
        Subject = new ClaimsIdentity(new[] {
          new Claim("type", "sign-in"),
          new Claim("email", emailBody.Email.ToString()),
          new Claim("unique", unique),
        }),
        Expires = DateTime.Now.AddMinutes(1),
        Audience = config["Jwt:Audience"],
        Issuer = config["Jwt:Issuer"],
        SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512)
      };

      var token = jwtTokenHandler.CreateToken(tokenDescriptor);
      var jwtToken = jwtTokenHandler.WriteToken(token);

      response.Cookies.Append("unique", unique, new CookieOptions {
        SameSite = SameSiteMode.Lax,
        HttpOnly = true,
        Path = "/auth/sign-in",
        Expires = DateTime.UtcNow.AddDays(30),
        MaxAge = TimeSpan.FromDays(30)
      });

      Console.WriteLine($"Sign in verification link with to {emailBody.Email}. {jwtToken}");
      return Results.Ok($"Sign in verification link with to {emailBody.Email}. {jwtToken}");
    });

    app.MapPost("/auth/refreshtoken", async (HttpRequest request, DataContext context, IConfiguration config) => {
      var refreshToken = request.Cookies["refreshToken"];
      if(refreshToken == null) return Results.BadRequest();

      var sessionFound = await context.Sessions.Include(session => session.User).FirstOrDefaultAsync(session => session.RefreshToken == refreshToken);
      if(sessionFound == null) return Results.BadRequest();

      var secureKey = Encoding.UTF8.GetBytes(config["Jwt:Key"]);
      var securityKey = new SymmetricSecurityKey(secureKey);
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
      var jwtTokenHandler = new JwtSecurityTokenHandler();

      var tokenDescriptor = new SecurityTokenDescriptor {
        Subject = new ClaimsIdentity(new[] {
          new Claim("userId", sessionFound.User.Id.ToString())
        }),
        Expires = DateTime.Now.AddMinutes(1),
        Audience = config["Jwt:Audience"],
        Issuer = config["Jwt:Issuer"],
        SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512)
      };

      var token = jwtTokenHandler.CreateToken(tokenDescriptor);

      return Results.Ok(new {
        accessToken = jwtTokenHandler.WriteToken(token)
      });
    });
  }
}
