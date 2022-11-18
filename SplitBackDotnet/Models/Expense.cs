namespace SplitBackDotnet.Models;
using System.ComponentModel.DataAnnotations;

public class Expense
{
public int ExpenseId { get; set; }

[MaxLength(200)]
[Required]
public string Description { get; set; } = String.Empty;
[Required]
public decimal Amount { get; set; }
//[Required]
//public ICollection<User> Spenders { get; set; } = new List<User>();
//public ICollection<User> Participants { get; set; } = new List<User>();
public Label? Label { get; set; }
[Required]
public ICollection<ExpenseUser> ExpenseUsers { get; set; } = new List<ExpenseUser>();
[Required]
public ICollection<Share> Shares { get; set; } = new List<Share>();

}
