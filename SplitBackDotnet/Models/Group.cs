using System.ComponentModel.DataAnnotations;
namespace SplitBackDotnet.Models;
using MongoDB.Bson;


public class Group
{
  public ObjectId Id { get; set; }
  [MaxLength(50)]
  [Required]
  public string Title { get; set; } = String.Empty;
  public ObjectId CreatorId { get; set; }
  [Required]
  public ICollection<ObjectId> Members { get; set; } = new List<ObjectId>();
  public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
  public ICollection<Transfer> Transfers { get; set; } = new List<Transfer>();
  public ICollection<Label> GroupLabels { get; set; } = new List<Label>();
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public string BaseCurrencyIsoCode { get; set; } = null!;
  public bool isArchived { get; set; } = false;
  public bool isDeleted { get; set; } = false;
}