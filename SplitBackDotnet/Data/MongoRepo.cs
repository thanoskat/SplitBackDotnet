using SplitBackDotnet.Models;
using Microsoft.EntityFrameworkCore;
using SplitBackDotnet.Dtos;
using AutoMapper;
using SplitBackDotnet.Extensions;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace SplitBackDotnet.Data;

public class MongoRepo : IRepo
{
  private readonly IMapper _mapper;
  private readonly IMongoCollection<Group> _groupCollection;
  private readonly IMongoCollection<Expense> _expenseCollection;
  private readonly IMongoCollection<User> _userCollection;
  private readonly IMongoCollection<Invitation> _invitationCollection;
  private readonly IMongoCollection<Session> _sessionCollection;
  private readonly string _connectionString;
  public MongoRepo(IMapper mapper, IOptions<AlphaSplitDatabaseSettings> alphaSplitDatabaseSettings)
  {
    var mongoClient = new MongoClient(alphaSplitDatabaseSettings.Value.ConnectionString);
    var mongoDatabase = mongoClient.GetDatabase(alphaSplitDatabaseSettings.Value.DatabaseName);
    _groupCollection = mongoDatabase.GetCollection<Group>(alphaSplitDatabaseSettings.Value.GroupCollection);
    _expenseCollection = mongoDatabase.GetCollection<Expense>(alphaSplitDatabaseSettings.Value.ExpenseCollection);
    _userCollection = mongoDatabase.GetCollection<User>(alphaSplitDatabaseSettings.Value.UserCollection);
    _sessionCollection = mongoDatabase.GetCollection<Session>(alphaSplitDatabaseSettings.Value.SessionCollection);
    _invitationCollection = mongoDatabase.GetCollection<Invitation>(alphaSplitDatabaseSettings.Value.InvitationCollection);
    _connectionString = alphaSplitDatabaseSettings.Value.ConnectionString;

    _mapper = mapper;

  }

  public async Task<bool> EmailExists(string email)
  {
    //var filter = Builders<User>.Filter.Eq("Email", email);
    var userCount = await _userCollection.CountDocumentsAsync(user => user.Email == email);
    return userCount > 0;

  }

  public async Task<Session> GetSessionByUnique(string unique)
  {
    return await _sessionCollection.Find(session => session.Unique == unique).SingleOrDefaultAsync();
  }

  public async Task<User> GetUserByEmail(string email)
  {
    return await _userCollection.Find(user => user.Email == email).SingleOrDefaultAsync();
  }

  public async Task<User> GetUserById(ObjectId userId)
  {
    return await _userCollection.Find(user => user.Id == userId).SingleOrDefaultAsync();
  }

  public async Task<Session> GetSessionByRefreshToken(string refreshToken)
  {
    return await _sessionCollection.Find(session => session.RefreshToken == refreshToken).SingleOrDefaultAsync();
  }
  public async Task AddUser(User user)
  {
    if (user is null)
    {
      throw new ArgumentNullException(nameof(user));
    }
    await _userCollection.InsertOneAsync(user);
  }
  public async Task AddSession(Session session)
  {
    if (session is null)
    {
      throw new ArgumentNullException(nameof(session));
    }
    await _sessionCollection.InsertOneAsync(session);

  }

  public Task AddLabel(Label label)
  {
    throw new NotImplementedException();
  }

  public async Task AddNewExpense(NewExpenseDto newExpenseDto)
  {
    var newExpense = _mapper.Map<Expense>(newExpenseDto);
    newExpense.CreatedAt = DateTime.Now;

    var groupId = ObjectId.Parse(newExpenseDto.GroupId);
    var updateExpenses = Builders<Group>.Update.AddToSet("Expenses", newExpense);
    await _groupCollection.FindOneAndUpdateAsync(group => group.Id == groupId, updateExpenses);
  }
  public async Task AddExpenseToHistory(Group oldGroup, ObjectId expenseId, FilterDefinition<Group>? filter)
  {
    var oldExpense = oldGroup.Expenses.First(e => e.Id == expenseId);
    var snapShot = _mapper.Map<ExpenseSnapShot>(oldExpense);
    var updateExpense = Builders<Group>.Update.Push("Expenses.$.History", snapShot);
    await _groupCollection.FindOneAndUpdateAsync(filter, updateExpense);
  }
  public async Task EditExpense(EditExpenseDto editExpenseDto)
  {
    var newExpense = _mapper.Map<Expense>(editExpenseDto);
    var groupId = ObjectId.Parse(editExpenseDto.GroupId);
    //var expenseId = ObjectId.Parse(editExpenseDto.ExpenseId);
    var expenseId = ObjectId.Parse("63aa01d6fc5958282c420af2");
    var filter = Builders<Group>.Filter.Eq("_id", groupId) & Builders<Group>.Filter.ElemMatch(g => g.Expenses, e => e.Id == expenseId);
    var updateExpense = Builders<Group>.Update
           .Set("Expenses.$.Description", newExpense.Description)
           .Set("Expenses.$.Amount", newExpense.Amount)
           .Set("Expenses.$.ExpenseSpenders", newExpense.ExpenseSpenders)
           .Set("Expenses.$.ExpenseParticipants", newExpense.ExpenseParticipants)
           .Set("Expenses.$.Label", newExpense.Label)
           .Set("Expenses.$.IsoCode", newExpense.IsoCode);

    var client = new MongoClient(_connectionString);
    using var session = await client.StartSessionAsync();
    session.StartTransaction();
    try
    {
      var oldGroup = await _groupCollection.FindOneAndUpdateAsync(filter, updateExpense);
      await AddExpenseToHistory(oldGroup, expenseId, filter);
    }
    catch (Exception ex)
    {
      await session.AbortTransactionAsync();
      Console.WriteLine(ex.Message);
    }
  }

  public async Task AddComment(NewCommentDto comment, ObjectId userId)
  {
    var newComment = _mapper.Map<Comment>(comment);
    newComment.CommentorId = userId;
    var expenseId = ObjectId.Parse(comment.ExpenseId);
    var groupId = ObjectId.Parse(comment.GroupId);

    var filter = Builders<Group>.Filter.Eq("_id", groupId) & Builders<Group>.Filter.ElemMatch(g => g.Expenses, e => e.Id == expenseId);
    var updateExpense = Builders<Group>.Update.Push("Expenses.$.Comments", newComment);
    try
    {
      await _groupCollection.FindOneAndUpdateAsync(filter, updateExpense);
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
  }

  public async Task AddNewTransfer(NewTransferDto newTransferDto)
  {
    var newTransfer = _mapper.Map<Transfer>(newTransferDto);
    newTransfer.CreatedAt = DateTime.Now;

    var groupId = ObjectId.Parse(newTransferDto.GroupId);
    var updateTransfers = Builders<Group>.Update.AddToSet("Transfers", newTransfer);
    await _groupCollection.FindOneAndUpdateAsync(group => group.Id == groupId, updateTransfers);
  }

  public async Task AddUserToGroup(ObjectId groupID, ObjectId userID)
  {
    var filter = Builders<Group>.Filter.Eq("_id", groupID) & Builders<Group>.Filter.AnyEq("Members", userID);
    var userCount = await _groupCollection.CountDocumentsAsync(filter);

    if (userCount == 0)
    {
      //update group
      var updateGroup = Builders<Group>.Update.AddToSet("Members", userID);
      await _groupCollection.FindOneAndUpdateAsync(group => group.Id == groupID, updateGroup);
      //update user
      var updateUser = Builders<User>.Update.AddToSet("Groups", groupID);
      await _userCollection.FindOneAndUpdateAsync(user => user.Id == userID, updateUser);
    }
    else throw new Exception();
  }

  public async Task CreateGroup(Group group)
  {
    if (group is null)
    {
      throw new ArgumentNullException(nameof(group));
    }
    var client = new MongoClient(_connectionString);
    using var session = await client.StartSessionAsync();
    session.StartTransaction();
    try
    {
      await _groupCollection.InsertOneAsync(group);
      await AddUserToGroup(group.Id, group.CreatorId);
      await session.CommitTransactionAsync();
    }
    catch (Exception ex)
    {
      await session.AbortTransactionAsync();
      Console.WriteLine(ex.Message);
    }
  }
  public async Task<Group?> GetGroupById(ObjectId groupId)
  {
    return await _groupCollection.Find(Builders<Group>.Filter.Eq("_id", groupId)).FirstOrDefaultAsync();
  }


  public async Task RemoveExpense(RemoveExpenseDto removeExpenseDto)
  {
    var groupId = ObjectId.Parse(removeExpenseDto.GroupId);
    var expenseId = ObjectId.Parse(removeExpenseDto.ExpenseId);
    var removeExpense = Builders<Group>.Update.PullFilter(g => g.Expenses, e => e.Id == expenseId);
    await _groupCollection.FindOneAndUpdateAsync(group => group.Id == groupId, removeExpense);
  }

  public async Task RemoveTransfer(RemoveTransferDto removeTransferDto)
  {
    var groupId = ObjectId.Parse(removeTransferDto.GroupId);
    var transferId = ObjectId.Parse(removeTransferDto.TransferId);
    var removeTransfer = Builders<Group>.Update.PullFilter(g => g.Transfers, t => t.Id == transferId);
    await _groupCollection.FindOneAndUpdateAsync(group => group.Id == groupId, removeTransfer);
  }

}