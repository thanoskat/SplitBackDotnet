using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using SplitBackDotnet.Data;
using SplitBackDotnet.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SplitBackDotnet.Endpoints.Authentication;

public static partial class AuthenticationEndpoints {

    private static async Task<IResult> RequestSignUp(HttpResponse response, IMapper mapper, IConfiguration config, IRepo repo, UserCreateDto userCreateDto) {
        if(await repo.EmailExists(userCreateDto.Email)) {
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
            Expires = DateTime.Now.AddMinutes(10),
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
        return Results.Ok($"{jwtToken}");
    }
}