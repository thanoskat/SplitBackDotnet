using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using SplitBackDotnet.Models;

namespace SplitBackDotnet.Extensions;

public static class BsonDocumentExtensions {
  
  public static IEvent? ToEvent(this BsonDocument doc) {
    
    var eventTypeInt = doc.GetValue("Type").AsInt32;
    
    if(!Enum.IsDefined(typeof(EventType), eventTypeInt)) {
      return null;
    }
    
    var eventTypeEnum = (EventType)eventTypeInt;
    
    var eventType = Type.GetType(string.Concat("SplitBackDotnet.Models.", eventTypeEnum.ToString()));
    
    return BsonSerializer.Deserialize(doc, eventType) as IEvent;
  }
}
