
namespace SplitBackDotnet.Models;

public class PendingTransaction
{
  public int Id { get; set; }
  public int SenderId { get; set; }
  public int ReceiverId { get; set; }
  public decimal Amount { get; set; }
  public Group Group { get; set; } = null!;
  public int CurrentGroupId { get; set; }
}