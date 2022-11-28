using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SplitBackDotnet.Dtos;
using SplitBackDotnet.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SplitBackDotnet.Services.JwtFactory;

public class JwtFactory : IJwtFactory {

  private readonly JwtSettings _jwtSettings;

  public ClaimsIdentity? Claims { get; set; }

  public JwtFactory(IOptions<JwtSettings> jwtSettings) {
    _jwtSettings = jwtSettings.Value;
  }

  public string GenerateToken() {
    var secureKey = Encoding.UTF8.GetBytes(_jwtSettings.Key);
    var securityKey = new SymmetricSecurityKey(secureKey);
    var jwtTokenHandler = new JwtSecurityTokenHandler();

    var tokenDescriptor = new SecurityTokenDescriptor {
      Subject = Claims,
      Expires = DateTime.Now.AddMinutes(1),
      Issuer = _jwtSettings.Issuer,
      Audience = _jwtSettings.Audience,
      SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512)
    };

    var token = jwtTokenHandler.CreateToken(tokenDescriptor);
    return jwtTokenHandler.WriteToken(token);
  }
}
