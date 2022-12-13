using SplitBackDotnet.Models;
using Microsoft.EntityFrameworkCore;
using SplitBackDotnet.Dtos;
using AutoMapper;
using SplitBackDotnet.Extensions;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace SplitBackDotnet.Data;

public class MongoRepo : IRepo
{
  private readonly IMapper _mapper;
  private readonly IMongoCollection<Group> _groupCollection;
  private readonly IMongoCollection<Expense> _expenseCollection;
  private readonly IMongoCollection<User> _userCollection;
  private readonly IMongoCollection<Invitation> _invitationCollection;
  private readonly IMongoCollection<Session> _sessionCollection;

  public MongoRepo(IMapper mapper, IOptions<AlphaSplitDatabaseSettings> alphaSplitDatabaseSettings)
  {
    var mongoClient = new MongoClient(alphaSplitDatabaseSettings.Value.ConnectionString);
    var mongoDatabase = mongoClient.GetDatabase(alphaSplitDatabaseSettings.Value.DatabaseName);
    _groupCollection = mongoDatabase.GetCollection<Group>(alphaSplitDatabaseSettings.Value.GroupCollection);
    _expenseCollection = mongoDatabase.GetCollection<Expense>(alphaSplitDatabaseSettings.Value.ExpenseCollection);
    _userCollection = mongoDatabase.GetCollection<User>(alphaSplitDatabaseSettings.Value.UserCollection);
    _sessionCollection = mongoDatabase.GetCollection<Session>(alphaSplitDatabaseSettings.Value.SessionCollection);
    _invitationCollection = mongoDatabase.GetCollection<Invitation>(alphaSplitDatabaseSettings.Value.InvitationCollection);

    _mapper = mapper;

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

  public async Task CreateGroup(Group group)
  {
    if (group == null)
    {
      throw new ArgumentNullException(nameof(group));
    }
    await _groupCollection.InsertOneAsync(group);
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

  public Task SaveChangesAsync()
  {
    throw new NotImplementedException();
  }
}