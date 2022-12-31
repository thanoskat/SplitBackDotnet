using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization.Conventions;
using SplitBackDotnet.Models;

namespace SplitBackDotnet.Data;

public class ActionDiscriminatorConvention : IDiscriminatorConvention {
    
  public string ElementName 
  {
    get { return "ActionType"; }
  }

  public Type GetActualType(IBsonReader bsonReader, Type nominalType) {
    
    var bookmark = bsonReader.GetBookmark();
    bsonReader.ReadStartDocument();
    
    if(bsonReader.FindElement(ElementName))
    {
      var value = bsonReader.ReadString();
    
      bsonReader.ReturnToBookmark(bookmark);
      
      if(value == "CreateAnAccountActionData")
      {
        return typeof(CreateAnAccountActionData);
      }
      
    }
    throw new NotSupportedException();
  }

  public BsonValue GetDiscriminator(Type nominalType, Type actualType) {
    
    return actualType.Name;
  }
}
