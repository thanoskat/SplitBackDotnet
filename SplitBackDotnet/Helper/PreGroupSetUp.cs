using SplitBackDotnet.Models;
using SplitBackDotnet.Data;

namespace SplitBackDotnet.Helper
{
  public static class PreGroupSetUp
  {
    public static void AddCreatorToMembers(DataContext context, HttpContext httpContext, Group Group)
    {
      int UserId = Convert.ToInt32(httpContext.User.FindFirst("userId").Value);
      User? UserFound = context.Users.FirstOrDefault(user => user.UserId == UserId);
      Group.Members.Add(UserFound);
      Group.Creator = UserFound;
    }
  }
}
