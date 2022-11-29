using SplitBackDotnet.Models;
using SplitBackDotnet.Dtos;
using AutoMapper;

namespace SplitBackDotnet.Data {
  public interface IRepo {
    Task SaveChangesAsync();
    Task CreateGroup(Group group);
    Task AddLabel (Label label);
    Task AddNewExpense (Currency Currency, NewExpenseDto newExpenseDto, Group Group, IMapper mapper);
    Task<Group?> GetGroupById(int groupId);

  }
}

