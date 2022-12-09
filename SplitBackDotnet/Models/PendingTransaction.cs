namespace SplitBackDotnet.Models;
using System.ComponentModel.DataAnnotations;

public class PendingTransaction
{

  public int SenderId { get; set; }

  public int ReceiverId { get; set; }

  public decimal Amount { get; set; }
  [MaxLength(3)]
  public string IsoCode { get; set; } = null!;
}
