using SplitBackDotnet.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SplitBackDotnet.Data
{
  public class Repo : IRepo
  {
    private readonly DataContext _context;

    public Repo(DataContext context)
    {
      _context = context;
    }

    public async Task<Group?> GetGroupById(int groupId)
    {
      //return await _context.Groups.Include(group => group.Expenses).FirstOrDefaultAsync(group => group.Id == groupId);
      return await _context.Groups.FirstOrDefaultAsync(group => group.GroupId == groupId);
    }
    public async Task CreateGroup(Group group)
    {
      if (group == null)
      {
        throw new ArgumentNullException(nameof(group));
      }
      await _context.AddAsync(group);
    }
    public async Task AddLabel(Label label)
    {
      if (label == null)
      {
        throw new ArgumentNullException(nameof(label));
      }
      await _context.AddAsync(label);
    }
    public async Task AddExpenseUsers(ICollection<ExpenseUser> expenseUsers)
    {
      if (expenseUsers == null)
      {
        throw new ArgumentNullException(nameof(expenseUsers));
      }
      foreach (ExpenseUser expenseUser in expenseUsers)
      {
        await _context.AddAsync(expenseUser);
      }

    }
    //public async Task AddShares(ICollection<Share> shares)
    //{
    //if (shares == null)
    //{
    //throw new ArgumentNullException(nameof(shares));
    //}
    //foreach (Share share in shares)
    //{
    // await _context.AddAsync(share);
    //}

    //}

    public async Task AddNewExpense(Expense expense)
    {
      if (expense == null)
      {
        throw new ArgumentNullException(nameof(expense));
      }
      await _context.AddAsync(expense);
    }
    public async Task SaveChangesAsync()
    {
      await _context.SaveChangesAsync();
    }
  }
}
