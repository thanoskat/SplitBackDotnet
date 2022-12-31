using Microsoft.OpenApi.Models;

namespace SplitBackDotnet.Services;

public static class SwaggerExtension {

  public static void AddMySwagger(this IServiceCollection services) {
    var securityScheme = new OpenApiSecurityScheme() {
      Name = "Authorization",
      Type = SecuritySchemeType.ApiKey,
      Scheme = "Bearer",
      BearerFormat = "JWT",
      In = ParameterLocation.Header,
      Description = "JSON Web Token based security",
    };

    var securityReq = new OpenApiSecurityRequirement() {
      {
        new OpenApiSecurityScheme {
          Reference = new OpenApiReference {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
          }
        },
        Array.Empty<string>()
      }
    };

    var contactInfo = new OpenApiContact() {
      Name = "thanoskat",
      Email = "thanoskat@email.com",
      Url = new Uri("http://github.com/thanoskat")
    };

    var license = new OpenApiLicense() {
      Name = "Free License",
    };

    var info = new OpenApiInfo() {
      Version = "V1",
      Title = "Split Back MinimalApi",
      Description = "Api for split back",
      Contact = contactInfo,
      License = license
    };

    services.AddSwaggerGen(options => {
      options.SwaggerDoc("v1", info);
      options.AddSecurityDefinition("Bearer", securityScheme);
      options.AddSecurityRequirement(securityReq);
    });
  }
}
