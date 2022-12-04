using System.ComponentModel.DataAnnotations;
namespace SplitBackDotnet.Models;

public class Group
{
  public int GroupId { get; set; }
  [MaxLength(50)]
  [Required]
  public string Title { get; set; } = String.Empty;
  [Required]
  public User Creator { get; set; } = new User();
  [Required]
  public ICollection<User> Members { get; set; } = new List<User>();
  public ICollection<Expense>? Expenses { get; set; } = new List<Expense>();
  public ICollection<Transfer>? Transfers { get; set; } = new List<Transfer>();
  public ICollection<Label>? Labels { get; set; } = new List<Label>();
}