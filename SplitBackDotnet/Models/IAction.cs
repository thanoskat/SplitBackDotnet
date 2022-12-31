using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SplitBackDotnet.Models;

public interface IAction
{
  public ObjectId Id { get; set; }
  
  public int ActionType { get; set; }
  
}