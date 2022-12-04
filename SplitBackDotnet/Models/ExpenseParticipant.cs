namespace SplitBackDotnet.Models;
using System.ComponentModel.DataAnnotations;
public class ExpenseParticipant
{
  public int ParticipantId { get; set; }
  public User Participant { get; set; } = null!;
  public int ExpenseId { get; set; }
  public Expense Expense { get; set; } = null!;
  public decimal ContributionAmount { get; set; }
}