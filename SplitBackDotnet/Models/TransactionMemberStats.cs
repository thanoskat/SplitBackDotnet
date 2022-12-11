namespace SplitBackDotnet.Models;

public class TransactionMemberStats
{
  public int TransactionId { get; set; }

  public DateTime CreatedAt { get; set; }

  public string Description { get; set; } = null!;

  public decimal Lent { get; set; }

  public decimal Borrowed { get; set; }

  public decimal UserPaid { get; set; }

  public decimal UserShare { get; set; }

  public bool IsTransfer { get; set; }

  // public decimal TotalLent { get; set; }

  // public decimal TotalBorrowed { get; set; }

  // public decimal Balance { get; set; }

  public string IsoCode { get; set; } = null!;
}