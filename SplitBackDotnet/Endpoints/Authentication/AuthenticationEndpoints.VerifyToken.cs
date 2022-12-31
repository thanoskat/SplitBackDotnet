using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SplitBackDotnet.Data;
using SplitBackDotnet.Helper;
using SplitBackDotnet.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SplitBackDotnet.Endpoints.Authentication;

public static partial class AuthenticationEndpoints {

    public static async Task<IResult> VerifyToken(HttpResponse response, IConfiguration config, IRepo repo, [FromBody] TokenBody tokenBody) {
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

            var sessionFound = await repo.GetSessionByUnique(unique);//await context.Sessions.FirstOrDefaultAsync(session => session.Unique == unique);
            if(sessionFound != null) return Results.BadRequest("Verification link already used");

            var newRefreshToken = Guid.NewGuid().ToString();

            if(type == "sign-up") {
                var email = validatedJwtToken.Payload.Claims.First(claim => claim.Type == "email").Value;
                var nickanme = validatedJwtToken.Payload.Claims.First(claim => claim.Type == "nickname").Value;

                var newUser = new User {
                    Email = email,
                    Nickname = nickanme,
                };
                await repo.AddUser(newUser);

                var newSession = new Session {
                    RefreshToken = newRefreshToken,
                    UserId = newUser.Id,
                    Unique = unique
                };
                await repo.AddSession(newSession);


                return Results.Ok(new { type = "sign-up" });

            } else if(type == "sign-in") {
                var email = validatedJwtToken.Payload.Claims.First(claim => claim.Type == "email").Value;
                var userFound = await repo.GetUserByEmail(email);
                if(userFound == null) return Results.NotFound("User does not exist");

                var newSession = new Session {
                    RefreshToken = newRefreshToken,
                    UserId = userFound.Id,
                    Unique = unique
                };
                await repo.AddSession(newSession);


                return Results.Ok(new { type = "sign-in" });
            } else return Results.BadRequest("Invalid token");

        } catch(Exception e) {
            Console.WriteLine(e.Message);
            return Results.Ok("JWT validation error");
        }
    }
}