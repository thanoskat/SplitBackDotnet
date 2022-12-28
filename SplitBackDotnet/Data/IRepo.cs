using SplitBackDotnet.Models;
using SplitBackDotnet.Dtos;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SplitBackDotnet.Data;
public interface IRepo
{
  Task CreateGroup(Group group);

  Task AddComment(NewCommentDto newComment, ObjectId userId);
  
  Task AddToHistory(Group oldGroup, ObjectId Id, FilterDefinition<Group>? filter, bool isExpense);

  Task AddLabel(Label label);

  Task AddNewExpense(NewExpenseDto newExpenseDto);

  Task EditExpense(EditExpenseDto editExpenseDto);

  Task RemoveExpense(RemoveExpenseDto removeExpenseDto);

  Task AddNewTransfer(NewTransferDto newTransferDto);

  Task EditTransfer(EditTransferDto editTransferDto);

  Task RemoveTransfer(RemoveTransferDto removeTransferDto);

  Task<Group?> GetGroupById(ObjectId groupId);

  Task<bool> EmailExists(string Email);

  Task<Session> GetSessionByUnique(string unique);

  Task AddUser(User user);

  Task AddSession(Session session);

  Task<User> GetUserByEmail(string email);

  Task<User> GetUserById(ObjectId userId);

  Task<Session> GetSessionByRefreshToken(string refreshToken);

  Task AddUserToGroup(ObjectId groupID, ObjectId UserID);

}
