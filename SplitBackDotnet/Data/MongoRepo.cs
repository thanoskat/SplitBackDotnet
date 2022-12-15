using SplitBackDotnet.Models;
using Microsoft.EntityFrameworkCore;
using SplitBackDotnet.Dtos;
using AutoMapper;
using SplitBackDotnet.Extensions;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

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

  public Task AddNewExpense(NewExpenseDto newExpenseDto)
  {
    throw new NotImplementedException();
  }

  public Task AddNewTransfer(NewTransferDto newTransferDto)
  {
    throw new NotImplementedException();
  }

  public async Task AddUserToGroup(ObjectId groupID, ObjectId userID)
  {
    var filter = Builders<Group>.Filter.Eq("_id", groupID) & Builders<Group>.Filter.AnyEq("Members", userID);
    var userCount = await _groupCollection.CountDocumentsAsync(filter);

    if (userCount == 0)
    {
      //update group
      var updateGroup = Builders<Group>.Update.AddToSet("Members",userID);
      await _groupCollection.FindOneAndUpdateAsync(group => group.Id == groupID, updateGroup);
      //update user
      //var filterUser = Builders<User>.Filter.Eq(user => user.Id, userID);
      var updateUser = Builders<User>.Update.AddToSet("Groups", groupID);
      await _userCollection.FindOneAndUpdateAsync(user => user.Id == userID, updateUser);
    }
  }

  public async Task CreateGroup(Group group)
  {
    if (group == null)
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


  public Task EditExpense(NewExpenseDto newExpenseDto)
  {
    throw new NotImplementedException();
  }

  public Task<Group?> GetGroupById(int groupId)
  {
    throw new NotImplementedException();
  }

  public Task RemoveExpense(RemoveExpenseDto removeExpenseDto)
  {
    throw new NotImplementedException();
  }

  public Task RemoveTransfer(RemoveTransferDto removeTransferDto)
  {
    throw new NotImplementedException();
  }

}