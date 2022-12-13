namespace SplitBackDotnet.Models;
using System.ComponentModel.DataAnnotations;

public class PendingTransaction
{
  public string SenderId { get; set; }

  public string ReceiverId { get; set; }
  public decimal Amount { get; set; }
  [MaxLength(3)]
  public string IsoCode { get; set; } = null!;
}
