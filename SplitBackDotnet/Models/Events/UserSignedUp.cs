using MongoDB.Bson;

namespace SplitBackDotnet.Models;

public class UserSignedUp : IEvent
{
  public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
  
  public EventType Type { get; set; } = EventType.UserSignedUp;
  
  public UserSignedUpData Data { get; set; } = null!;
}

public class UserSignedUpData
{
  public string Email { get; set; } = null!;
  
  public string Nickname { get; set; } = null!;
}
