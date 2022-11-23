using SplitBackDotnet.Models;
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
      //return await _context.Groups.FirstOrDefaultAsync(group => group.GroupId == groupId);
      return await _context.Groups
      .Include(group => group.Members)
      .Include(group => group.Expenses)
      .Include(group => group.Transfers)
      .Include(group => group.Expenses.Select(exp => exp.ExpenseParticipants)) 
      .Include(group => group.Expenses.Select(exp => exp.ExpenseSpenders))
      .FirstOrDefaultAsync(group => group.GroupId == groupId);
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
