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
public Label? Label { get; set; }
[Required]
public ICollection<ExpenseSpender> ExpenseSpenders { get; set; } = new List<ExpenseSpender>();
[Required]
public ICollection<ExpenseParticipant> ExpenseParticipants { get; set; } = new List<ExpenseParticipant>();

}
