using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SplitBackDotnet.Services;

public static class AuthenticationExtension {

  public static void AddMyAuthentication(this IServiceCollection services, IConfiguration config) {

    services.AddAuthentication(options => {
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options => {
      options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true,
        ValidIssuer = config["Jwt:Issuer"],
        ValidAudience = config["Jwt:Audience"],
        ValidateAudience = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"])),
        ValidateLifetime = true, // In any other application other then demo this needs to be true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero
      };
    });
    Console.WriteLine(config["Jwt:Key"]);
    services.AddAuthentication();
  }
}
