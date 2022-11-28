using System.Security.Claims;

namespace SplitBackDotnet.Services.JwtFactory {
  public interface IJwtFactory {
    ClaimsIdentity Claims { set; }

    public string GenerateToken();
  }
}