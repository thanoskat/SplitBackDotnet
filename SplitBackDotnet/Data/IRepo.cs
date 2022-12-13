using SplitBackDotnet.Models;
using SplitBackDotnet.Dtos;

namespace SplitBackDotnet.Data;

public interface IRepo
{
  Task CreateGroup(Group group);

  Task AddLabel(Label label);

  Task AddNewExpense(NewExpenseDto newExpenseDto);

  Task EditExpense(NewExpenseDto newExpenseDto);

  Task RemoveExpense(RemoveExpenseDto removeExpenseDto);

  Task RemoveTransfer(RemoveTransferDto removeTransferDto);

  Task AddNewTransfer(NewTransferDto newTransferDto);

  Task<Group?> GetGroupById(int groupId);

  Task<bool> EmailExists(string Email);

  Task<Session> GetSessionByUnique(string unique);

  Task AddUser(User user);

  Task AddSession(Session session);

  Task<User> GetUserByEmail(string email);

  Task<User> GetUserById(string userId);

  Task<Session> GetSessionByRefreshToken(string refreshToken);

}
