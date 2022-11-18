namespace SplitBackDotnet.Models;

public class ExpenseUser
{
  public int SpenderId { get; set; }
  public User Spender { get; set; } = null!;
  public int ExpenseId { get; set; }
  public Expense Expense { get; set; } = null!;
  public decimal SpenderAmount { get; set; }

}