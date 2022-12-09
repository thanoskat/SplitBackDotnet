using SplitBackDotnet.Models;
using SplitBackDotnet.Dtos;

namespace SplitBackDotnet.Data;

public interface IRepo {

  Task SaveChangesAsync();

  Task CreateGroup(Group group);

  Task AddLabel(Label label);

  Task AddNewExpense(NewExpenseDto newExpenseDto);

  Task EditExpense(NewExpenseDto newExpenseDto);

  Task AddNewTransfer(NewTransferDto newTransferDto);

  Task<Group?> GetGroupById(int groupId);
}
