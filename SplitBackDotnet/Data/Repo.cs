using SplitBackDotnet.Models;
using Microsoft.EntityFrameworkCore;
using SplitBackDotnet.Dtos;
using AutoMapper;

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
      .Include(group => group.PendingTransactions)
      .Include(group => group.Expenses).ThenInclude(exp => exp.ExpenseParticipants)
      .Include(group => group.Expenses).ThenInclude(exp => exp.ExpenseSpenders)
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

    public async Task AddNewExpense(NewExpenseDto newExpenseDto, IMapper mapper)
    {
        var newExpense = mapper.Map<Expense>(newExpenseDto);
        await _context.AddAsync<Expense>(newExpense);
        await SaveChangesAsync();
      
    }
    public async Task SaveChangesAsync()
    {
      await _context.SaveChangesAsync();
    }
  }
}
