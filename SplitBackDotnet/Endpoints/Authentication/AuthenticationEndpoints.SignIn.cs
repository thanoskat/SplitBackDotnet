using Microsoft.IdentityModel.Tokens;
using SplitBackDotnet.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SplitBackDotnet.Endpoints.Authentication;


public static partial class AuthenticationEndpoints {

  private static async Task<IResult> SignIn(IConfiguration config, HttpRequest request, HttpResponse response, IRepo repo) {
    var unique = request.Cookies["unique"];
    if(unique == null) return Results.Unauthorized();

    var sessionFound = await repo.GetSessionByUnique(unique);
    if(sessionFound == null) return Results.Unauthorized();

    var userFound = await repo.GetUserById(sessionFound.UserId);
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
          new Claim("userId", sessionFound.UserId)
        }),
      Expires = DateTime.Now.AddMinutes(10),
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
  }
}