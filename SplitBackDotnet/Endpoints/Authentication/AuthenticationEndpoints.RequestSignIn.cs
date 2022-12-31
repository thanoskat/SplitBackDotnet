using Microsoft.IdentityModel.Tokens;
using SplitBackDotnet.Data;
using SplitBackDotnet.Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SplitBackDotnet.Endpoints.Authentication;

public static partial class AuthenticationEndpoints {

    private static async Task<IResult> RequestSignIn(EmailBody emailBody, HttpResponse response, IConfiguration config, IRepo repo) {
        var userFound = await repo.GetUserByEmail(emailBody.Email);
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

        Console.WriteLine($"Sign in verification link with to {emailBody.Email}. {jwtToken}");
        return Results.Ok($"{jwtToken}");
    }
}