using SplitBackDotnet.Models;

namespace SplitBackDotnet.Extensions;

public static class TransferExtensions
{

  public static TransactionMemberStats? ToTransactionMemberStatsFromUserId(this Transfer transfer, int userId)
  {
    if(transfer is null) return null;
    
    var isSender = transfer.SenderId == userId;
    var isReceiver = transfer.ReceiverId == userId;
    decimal lent = 0;
    decimal borrowed = 0;
    decimal paid = 0;
    decimal participation = 0;

    if (!isSender && !isReceiver) return null;

    if (isSender) {
      lent = transfer.Amount;
    }

    if (isReceiver) {
      borrowed = transfer.Amount;
    }

    return new TransactionMemberStats {
      TransactionId = transfer.TransferId,
      CreatedAt = transfer.CreatedAt,
      Description = transfer.Description,
      Lent = lent,
      Borrowed = borrowed,
      UserPaid = paid,
      UserShare = participation,
      IsTransfer = true,
      IsoCode = transfer.IsoCode
    };
  }

}
