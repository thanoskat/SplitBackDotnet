namespace SplitBackDotnet.Models;
using System.ComponentModel.DataAnnotations;

public class Expense
{
  public int ExpenseId { get; set; }

  [MaxLength(200)]
  public string Description { get; set; } = null!;
  [Required]
  public decimal Amount { get; set; }
  [MaxLength(3)]
  public string isoCode { get; set; } = null!;
  public Label? Label { get; set; }
  [Required]
  public ICollection<ExpenseSpender> ExpenseSpenders { get; set; } = new List<ExpenseSpender>();
  [Required]
  public ICollection<ExpenseParticipant> ExpenseParticipants { get; set; } = new List<ExpenseParticipant>();
  public Group Group { get; set; } = null!;
  public int GroupId { get; set; }
}
