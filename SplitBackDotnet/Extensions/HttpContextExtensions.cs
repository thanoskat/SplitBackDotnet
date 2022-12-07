using SplitBackDotnet.Models;

namespace SplitBackDotnet.Extensions;

public static class HttpContextExtensions {
  
  public static int GetAuthorizedUserId(this HttpContext httpContext) {
    
      var userClaim = httpContext.User.FindFirst("userId");
      
      if (userClaim is null) throw new Exception();
      
      if(!Int32.TryParse(userClaim.Value, out int userId)) throw new Exception();
      
      return userId;
  }
}