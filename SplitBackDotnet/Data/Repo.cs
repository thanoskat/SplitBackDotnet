using SplitBackDotnet.Models;
using Microsoft.EntityFrameworkCore;
using SplitBackDotnet.Dtos;
using AutoMapper;
using SplitBackDotnet.Extensions;

namespace SplitBackDotnet.Data;

public class Repo : IRepo
{

  private readonly DataContext _context;
  private readonly IMapper _mapper;

  public Repo(DataContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<Group?> GetGroupById(int groupId)
  {
    //return await _context.Groups.Include(group => group.Expenses).FirstOrDefaultAsync(group => group.Id == groupId);
    //return await _context.Groups.FirstOrDefaultAsync(group => group.GroupId == groupId);
    return await _context.Groups
    .Include(group => group.Members)
    .Include(group => group.Expenses)
    .Include(group => group.Transfers)
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
    group.CreatedAt = DateTime.Now;
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

  public async Task AddNewExpense(NewExpenseDto newExpenseDto)
  {

    var newExpense = _mapper.Map<Expense>(newExpenseDto);
    newExpense.CreatedAt = DateTime.Now;
    await _context.AddAsync<Expense>(newExpense);
    await SaveChangesAsync();
  }

  public async Task EditExpense(NewExpenseDto newExpenseDto)
  {
    var findExpenseToUpdate = await _context.Expenses
    .Include(exp => exp.ExpenseParticipants)
    .Include(exp => exp.ExpenseSpenders)
    .SingleAsync(exp => exp.ExpenseId == 6);

    var newExpense = _mapper.Map<Expense>(newExpenseDto);
    findExpenseToUpdate.Description = newExpense.Description;
    findExpenseToUpdate.Amount = newExpense.Amount;
    findExpenseToUpdate.ExpenseParticipants = newExpense.ExpenseParticipants;
    findExpenseToUpdate.ExpenseSpenders = newExpense.ExpenseSpenders;

    // var newExpense = _mapper.Map<Expense>(newExpenseDto);
    // newExpense.ExpenseId = 6;
    // _context.Update<Expense>(newExpense);

    await SaveChangesAsync();
  }

  public async Task RemoveExpense(RemoveExpenseDto removeExpenseDto)
  {
    var expenseToRemove = await _context.Expenses
    .Include(exp => exp.ExpenseParticipants)
    .Include(exp => exp.ExpenseSpenders)
    .SingleAsync(exp => exp.ExpenseId == removeExpenseDto.ExpenseId.ToInt());
    _context.Expenses.Remove(expenseToRemove);
    await SaveChangesAsync();
  }

  public async Task AddNewTransfer(NewTransferDto newTransferDto)
  {

    var newTransfer = _mapper.Map<Transfer>(newTransferDto);
    newTransfer.CreatedAt = DateTime.Now;
    await _context.AddAsync<Transfer>(newTransfer);
    await SaveChangesAsync();
  }

  public async Task RemoveTransfer(RemoveTransferDto removeTransferDto)
  {
    var transferToRemove = await _context.Transfers
    .SingleAsync(tr => tr.TransferId == removeTransferDto.TransferId.ToInt());
    _context.Transfers.Remove(transferToRemove);
    await SaveChangesAsync();
  }

  public async Task SaveChangesAsync()
  {
    await _context.SaveChangesAsync();
  }
}
