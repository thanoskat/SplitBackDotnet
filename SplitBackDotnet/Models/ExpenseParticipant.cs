namespace SplitBackDotnet.Models;
using MongoDB.Bson;


public class ExpenseParticipant
{
  public ObjectId Id { get; set; }
  public decimal ContributionAmount { get; set; }
}