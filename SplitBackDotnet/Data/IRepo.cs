using SplitBackDotnet.Models;
using SplitBackDotnet.Dtos;
using AutoMapper;

namespace SplitBackDotnet.Data {
  public interface IRepo {
    Task SaveChangesAsync();
    Task CreateGroup(Group group);
    Task AddLabel (Label label);
    Task AddNewExpense (NewExpenseDto newExpenseDto,IMapper mapper);
    Task AddNewTransfer(NewTransferDto newTransferDto, IMapper mapper);
    Task<Group?> GetGroupById(int groupId);

  }
}

