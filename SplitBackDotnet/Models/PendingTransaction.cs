
namespace SplitBackDotnet.Models;

public class PendingTransaction
{
  public int Id { get; set; }
  public int SenderId { get; set; }
  public int ReceiverId { get; set; }
  public decimal Amount { get; set; }
}