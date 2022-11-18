namespace SplitBackDotnet.Models;

public class Share
{
  public int ParticipantId { get; set; }
  public User Participant { get; set; } = null!;
  public int ExpenseId { get; set; }
  public Expense Expense { get; set; } = null!;
  public decimal ParticipantAmount { get; set; }
}
