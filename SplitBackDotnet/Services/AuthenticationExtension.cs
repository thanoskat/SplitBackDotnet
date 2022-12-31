using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SplitBackDotnet.Services;

public static class AuthenticationExtension {

  public static void AddMyAuthentication(this IServiceCollection services, IConfiguration config) {

    services.AddAuthentication(options => {
      // options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      // options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options => {
      options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true,
        ValidIssuer = config["Jwt:Issuer"],
        ValidAudience = config["Jwt:Audience"],
        ValidateAudience = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"])),
        ValidateLifetime = true, 
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero
      };
    });
    // services.AddAuthentication();
    // services.AddAuthorization();
    services.AddAuthorization(options =>
    {
        options.FallbackPolicy = new AuthorizationPolicyBuilder()
          .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
          .RequireAuthenticatedUser()
          .Build();
    });
  }
}
