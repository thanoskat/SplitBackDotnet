using MongoDB.Bson;

namespace SplitBackDotnet.Models;

public interface IEvent
{
  public ObjectId Id { get; set; }
  
  public EventType Type { get; set; }
}