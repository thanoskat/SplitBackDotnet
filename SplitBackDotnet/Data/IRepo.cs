using SplitBackDotnet.Models;

namespace SplitBackDotnet.Data {
  public interface IRepo {
    Task SaveChangesAsync();
    Task CreateGroup(Group group);
    Task AddLabel (Label label);
    //Task AddExpenseUsers(ICollection<ExpenseUser> expenseUsers);
    //Task AddShares(ICollection<Share> shares);
    Task AddNewExpense (Expense expense);
    Task<Group?> GetGroupById(int groupId);

    //Task AddUserToGroup(Group group, User user);
  }
}

