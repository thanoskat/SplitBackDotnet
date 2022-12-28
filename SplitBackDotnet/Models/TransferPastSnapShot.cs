using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
namespace SplitBackDotnet.Models;

public class TransferPastSnapShot
{
  public ObjectId Id { get; set; }
  [MaxLength(80)]
  public string? Description { get; set; }
  public decimal Amount { get; set; }
  public string IsoCode { get; set; } = null!;
  public ObjectId SenderId { get; set; }
  public ObjectId ReceiverId { get; set; }
  public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
  public bool IsDeleted { get; set; }
}