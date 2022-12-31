using MongoDB.Bson;

namespace SplitBackDotnet.Models;

public class UserSignedIn : IEvent
{
  public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
  
  public EventType Type { get; set; } = EventType.UserSignedUp;
  
  public UserSignedUpData Data { get; set; } = null!;
}

public class UserSignedInData
{
  public string Email { get; set; } = null!;
  
  public string Nickname { get; set; } = null!;
}
