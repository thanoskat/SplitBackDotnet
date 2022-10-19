using SplitBackDotnet.Models;
using SplitBackDotnet.Data;


namespace SplitBackDotnet.Helper {
  public class UserLookup {
    private readonly DataContext _context;

    public UserLookup(DataContext context) {
      _context = context;
    }
    public bool UserFound(User user, Group group) {
    return true;
    }
  }
}