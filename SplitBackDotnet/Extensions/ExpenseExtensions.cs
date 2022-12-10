using SplitBackDotnet.Models;

namespace SplitBackDotnet.Extensions;

public static class ExpenseExtensions
{

  public static TransactionMemberDetail? ToTransactionMemberDetailFromUserId(this Expense expense, int userId) {

    bool isSpender = expense.ExpenseSpenders.ToList().Any(es => es.SpenderId == userId);
    bool isParticipant = expense.ExpenseParticipants.ToList().Any(ep => ep.ParticipantId == userId);
    decimal lent = 0;
    decimal borrowed = 0;
    decimal paid = 0;
    decimal participation = 0;

    if(!isSpender && !isParticipant) return null;

    if(isSpender && isParticipant) {

      var spenderAmount = expense.ExpenseSpenders.ToList().Single(es => es.SpenderId == userId).SpenderAmount;
      var participantAmount = expense.ExpenseParticipants.ToList().Single(ep => ep.ParticipantId == userId).ContributionAmount;
      lent = spenderAmount - participantAmount;
      paid = spenderAmount;
      participation = participantAmount;
    }

    if (isSpender && !isParticipant) {

      var spenderAmount = expense.ExpenseSpenders.ToList().Single(es => es.SpenderId == userId).SpenderAmount;
      lent = spenderAmount;
      paid = spenderAmount;
    }

    if (!isSpender && isParticipant) {

      var participantAmount = expense.ExpenseParticipants.ToList().Single(ep => ep.ParticipantId == userId).ContributionAmount;
      borrowed = participantAmount;
    }

    return new TransactionMemberDetail {
      TransactionId = expense.ExpenseId,
      CreatedAt = expense.CreatedAt,
      Description = expense.Description,
      Lent = lent,
      Borrowed = borrowed,
      UserPaid = paid,
      UserShare = participation,
      IsTransfer = false,
      IsoCode = expense.IsoCode
    };
  }
}
