using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SplitBackDotnet.Models;

// [Serializable]
[BsonKnownTypes(typeof(CreateAnAccountAction))]
// [BsonDiscriminator("5")]
public class CreateAnAccountAction : IAction
{
  public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
  
  public int ActionType { get; set; }
  
  public CreateAnAccountActionData ActionData { get; set; }
}


public class CreateAnAccountActionData
{
  public string Email { get; set; }
  
  public int Age { get; set; }
  
}