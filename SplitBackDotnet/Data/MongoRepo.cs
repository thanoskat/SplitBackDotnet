using SplitBackDotnet.Models;
using Microsoft.EntityFrameworkCore;
using SplitBackDotnet.Dtos;
using AutoMapper;
using SplitBackDotnet.Extensions;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
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
  private readonly IMongoCollection<BsonDocument> _actionCollection;

  public MongoRepo(IMapper mapper, IOptions<AlphaSplitDatabaseSettings> alphaSplitDatabaseSettings)
  {
    
    var mongoClient = new MongoClient(alphaSplitDatabaseSettings.Value.ConnectionString);
    var mongoDatabase = mongoClient.GetDatabase(alphaSplitDatabaseSettings.Value.DatabaseName);
    _groupCollection = mongoDatabase.GetCollection<Group>(alphaSplitDatabaseSettings.Value.GroupCollection);
    _expenseCollection = mongoDatabase.GetCollection<Expense>(alphaSplitDatabaseSettings.Value.ExpenseCollection);
    _userCollection = mongoDatabase.GetCollection<User>(alphaSplitDatabaseSettings.Value.UserCollection);
    _sessionCollection = mongoDatabase.GetCollection<Session>(alphaSplitDatabaseSettings.Value.SessionCollection);
    _invitationCollection = mongoDatabase.GetCollection<Invitation>(alphaSplitDatabaseSettings.Value.InvitationCollection);
    _actionCollection = mongoDatabase.GetCollection<BsonDocument>(alphaSplitDatabaseSettings.Value.ActionCollection);
    
    _mapper = mapper;

  }
  
  public async Task Test2()
  {
    var userSignedUpEvent = new UserSignedUp
    {
      Data = new UserSignedUpData
      {
        Email = "mail@mail.com",
        Nickname = "nick"
      }
    };
    
    await _actionCollection.InsertOneAsync(userSignedUpEvent.ToBsonDocument());
    
    // var anAction = new CreateAnAccountAction
    // {
    //   ActionType = 7,
    //   ActionData = new CreateAnAccountActionData
    //   {
    //     Age = 15,
    //     Email = "mail@mail.com"
    //   }
    // };
    // await _actionCollection.InsertOneAsync(anAction);
    
    // var anActionWithoutDiscriminator = new BsonDocument
    // {
    //   new BsonElement("MyAction", new BsonDocument("Lala", "lala"))
    // };
    
    // await _actionCollection.InsertOneAsync(anActionWithoutDiscriminator);
  }
  
  
  public async Task Test()
  {
    var docs = await _actionCollection.Find(Builders<BsonDocument>.Filter.Empty).Limit(10).ToListAsync();
    
    var eventsWithData = docs.Select(doc => doc.ToEvent());
    
    var isUserSignedUpEvent = eventsWithData.First() is UserSignedIn;
    
    // var eventType = 
    
    // var filter = Builders<IAction>.Filter.Eq("_t", "CreateAnAccountAction");
    // var lala = _actionCollection.Find(_ => true).ToList();
    // var lala2 = lala.ToList();
    // var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse("63a37a1cef73d552bcf6ca90"));
    // var doc = _actionCollection.Find(filter).First();
    // var lala = BsonSerializer.Deserialize<CreateAnAccountAction>(doc);
  }

  public async Task<bool> EmailExists(string email)
  {
    var filter = Builders<User>.Filter.Eq("Email", email);
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

  public async Task<User> GetUserById(string userId)
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

}