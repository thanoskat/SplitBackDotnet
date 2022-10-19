using SplitBackDotnet.Models;

namespace SplitBackDotnet.Data {
  public interface IRepo {
    Task SaveChangesAsync();

    Task CreateGroup(Group group);

    //Task AddUserToGroup(Group group, User user);
  }
}

