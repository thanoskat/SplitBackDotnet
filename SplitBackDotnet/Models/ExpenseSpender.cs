namespace SplitBackDotnet.Models;
using System.ComponentModel.DataAnnotations;

public class ExpenseSpender
{
  public int SpenderId { get; set; }
  public User Spender { get; set; } = null!;
  public int ExpenseId { get; set; }
  public Expense Expense { get; set; } = null!;
  public decimal SpenderAmount { get; set; }

}