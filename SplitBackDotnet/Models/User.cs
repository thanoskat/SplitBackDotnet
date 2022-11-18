using System.ComponentModel.DataAnnotations;
namespace SplitBackDotnet.Models;

public class User
{
  public int UserId { get; set; }
  [MaxLength(200)]
  [Required]
  public string Nickname { get; set; } = String.Empty;
  [MaxLength(200)]
  [Required]
  public string Email { get; set; } = String.Empty;
  public ICollection<Group>? Groups { get; set; } = new List<Group>();
  public ICollection<Group>? CreatedGroups { get; set; } = new List<Group>();
  //public ICollection<Expense>? Expenses { get; set; } = new List<Expense>();
  [Required]
  public ICollection<ExpenseUser> ExpenseUsers { get; set; } = new List<ExpenseUser>();
  public ICollection<Share> Shares { get; set; } = new List<Share>();
}
