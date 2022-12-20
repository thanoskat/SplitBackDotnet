namespace SplitBackDotnet.Models;
using MongoDB.Bson;

public class ExpenseSpender
{
  public ObjectId Id { get; set; }
  public decimal SpenderAmount { get; set; }

}