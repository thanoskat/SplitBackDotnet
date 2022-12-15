namespace SplitBackDotnet.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class ExpenseSpender
{
  public ObjectId Id { get; set; }
  public decimal SpenderAmount { get; set; }

}