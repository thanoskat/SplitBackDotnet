namespace SplitBackDotnet.Models;

public class TransactionTimelineItem
{
  // public TransactionTimelineItem(
  //   TransactionMemberDetail transactionMemberDetail,
  //   decimal totalLent,
  //   decimal totalBorrowed,
  //   decimal balance)
  // {
  //   TransactionId = transactionMemberDetail.TransactionId;
  //   CreatedAt = transactionMemberDetail.CreatedAt;
  //   Description = transactionMemberDetail.Description;
  //   Lent = transactionMemberDetail.Lent;
  //   Borrowed = transactionMemberDetail.Borrowed;
  //   UserPaid = transactionMemberDetail.UserPaid;
  //   UserShare = transactionMemberDetail.UserShare;
  //   IsTransfer = transactionMemberDetail.IsTransfer;

  // }

  public int TransactionId { get; set; }

  public DateTime CreatedAt { get; set; }

  public string Description { get; set; } = String.Empty;

  public decimal Lent { get; set; }

  public decimal Borrowed { get; set; }

  public decimal UserPaid { get; set; }

  public decimal UserShare { get; set; }

  public bool IsTransfer { get; set; }

  public decimal TotalLent { get; set; }

  public decimal TotalBorrowed { get; set; }

  public decimal Balance { get; set; }

  public string IsoCode { get; set; } = String.Empty;
}