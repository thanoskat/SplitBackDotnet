using SplitBackDotnet.Models;

namespace SplitBackDotnet.Extensions;

public static class TransactionMemberStatsExtension
{

  public static TransactionTimelineItem ToTransactionTimelineItem(
    this TransactionMemberStats transactionMemberStats,
    decimal totalLentSoFar,
    decimal totalBorrowedSoFar)
  {
    return new TransactionTimelineItem
    {
      TransactionId = transactionMemberStats.TransactionId,
      CreatedAt = transactionMemberStats.CreatedAt,
      Description = transactionMemberStats.Description,
      Lent = transactionMemberStats.Lent,
      Borrowed = transactionMemberStats.Borrowed,
      UserPaid = transactionMemberStats.UserPaid,
      UserShare = transactionMemberStats.UserShare,
      IsTransfer = transactionMemberStats.IsTransfer,
      IsoCode = transactionMemberStats.IsoCode,

      TotalLent = totalLentSoFar,
      TotalBorrowed = totalBorrowedSoFar,
      Balance = totalLentSoFar - totalBorrowedSoFar
    };
  }
}
