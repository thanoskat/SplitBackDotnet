using Microsoft.IdentityModel.Tokens;
using SplitBackDotnet.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SplitBackDotnet.Endpoints.Authentication;

public static partial class AuthenticationEndpoints {

    private static async Task<IResult> RefreshToken(HttpRequest request, IConfiguration config, IRepo repo) {
        var refreshToken = request.Cookies["refreshToken"];
        if(refreshToken == null) return Results.BadRequest();

        var sessionFound = await repo.GetSessionByRefreshToken(refreshToken);
        if(sessionFound == null) return Results.BadRequest();

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
            accessToken = jwtTokenHandler.WriteToken(token)
        });
    }
}