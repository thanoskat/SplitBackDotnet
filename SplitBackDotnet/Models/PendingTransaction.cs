namespace SplitBackDotnet.Models;

public class PendingTransaction {

  public int SenderId { get; set; }

  public int ReceiverId { get; set; }

  public decimal Amount { get; set; }

  public string IsoCode { get; set; } = null!;
}

