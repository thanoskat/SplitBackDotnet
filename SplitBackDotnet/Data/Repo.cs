using SplitBackDotnet.Models;

namespace SplitBackDotnet.Data {
  public class Repo : IRepo {
    private readonly DataContext _context;

    public Repo(DataContext context) {
      _context = context;
    }

    public async Task CreateGroup(Group group) {
      if(group == null) {
        throw new ArgumentNullException(nameof(group));
      }
      await _context.AddAsync(group);
    }

    //public async Task AddUserToGroup(Group group, User user) {
      // if(group == null) {
      //   throw new ArgumentNullException(nameof(group));
      // } else if(user == null) {
      //   throw new ArgumentNullException(nameof(user));
      // } else {
        
      // }
    //}
    public async Task SaveChangesAsync() {
      await _context.SaveChangesAsync();
    }
  }
}
