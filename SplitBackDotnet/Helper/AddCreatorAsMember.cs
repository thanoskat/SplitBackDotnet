using SplitBackDotnet.Models;
using SplitBackDotnet.Data;

namespace SplitBackDotnet.Helper {
  public static class PreGroupSetUp {
     public static void AddCreatorToMembers(DataContext context,HttpContext httpContext,Group Group){
      int UserId = Convert.ToInt32(httpContext.User.FindFirst("userId").Value);
      User? UserFound = context.Users.FirstOrDefault(user => user.Id==UserId);
      ICollection<User> collection = new List<User>();
      collection.Add(UserFound);
      Group.Members=collection;
      Group.Creator=UserFound;
     }
    }
  }
