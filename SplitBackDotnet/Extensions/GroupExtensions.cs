using SplitBackDotnet.Models;
using SplitBackDotnet.Helper;

namespace SplitBackDotnet.Extensions;

public static class GroupExtensions
{
  public static IEnumerable<string> UniqueCurrencyCodes(this Group group)
  {
    var expenseListsByIsoCode = group.Expenses.GroupBy(exp => exp.IsoCode);
    var transferListsByIsoCode = group.Transfers.GroupBy(tr => tr.IsoCode);

    var isoCodeList = new List<string>();
    expenseListsByIsoCode.ToList().ForEach(list => isoCodeList.Add(list.Key));
    transferListsByIsoCode.ToList().ForEach(list => isoCodeList.Add(list.Key));
    var uniqueIsoCodeList = isoCodeList.Distinct();

    return uniqueIsoCodeList;
  }

  public static List<PendingTransaction> PendingTransactions(this Group group)
  {
    var uniqueIsoCodeList = IsoCodeHelper.GetUniqueIsoCodes(group);

    var pendingTransactions = new List<PendingTransaction>();

    uniqueIsoCodeList.ToList().ForEach(currentIsoCode =>
    {
      var participants = new List<Participant>();

      group.Members.ToList().ForEach(member =>
      {
        participants.Add(new Participant(member.UserId, 0m, 0m));
      });

      group.Expenses.Where(exp => exp.IsoCode == currentIsoCode).ToList().ForEach(expense =>
      {
        expense.ExpenseParticipants.ToList().ForEach(expenseParticipant =>
        {
          participants.Single(p => p.Id == expenseParticipant.ParticipantId).TotalAmountTaken += expenseParticipant.ContributionAmount;
        });

        expense.ExpenseSpenders.ToList().ForEach(expenseSpender =>
        {
          participants.Single(p => p.Id == expenseSpender.SpenderId).TotalAmountGiven += expenseSpender.SpenderAmount;
        });
      });

      group.Transfers.Where(exp => exp.IsoCode == currentIsoCode).ToList().ForEach(transfer =>
      {

        participants.Single(p => p.Id == transfer.ReceiverId).TotalAmountTaken += transfer.Amount;

        participants.Single(p => p.Id == transfer.SenderId).TotalAmountGiven += transfer.Amount;
      });

      var debtors = new Queue<Participant>();
      var creditors = new Queue<Participant>();

      participants.ForEach(p =>
      {

        switch (p.TotalAmountGiven - p.TotalAmountTaken)
        {

          case < 0:
            debtors.Enqueue(p);
            break;

          case > 0:
            creditors.Enqueue(p);
            break;
        }
      });

      while (debtors.Count > 0 && creditors.Count > 0)
      {

        var poppedDebtor = debtors.Dequeue();
        var poppedCreditor = creditors.Dequeue();

        var debt = (poppedDebtor.TotalAmountTaken - poppedDebtor.TotalAmountGiven);
        var credit = (poppedCreditor.TotalAmountGiven - poppedCreditor.TotalAmountTaken);
        var diff = debt - credit;

        switch (diff)
        {

          case < 0:
            pendingTransactions.Add(new PendingTransaction
            {
              SenderId = poppedDebtor.Id,
              ReceiverId = poppedCreditor.Id,
              Amount = debt,
              IsoCode = currentIsoCode,
            });

            creditors.Enqueue(poppedCreditor with { TotalAmountTaken = poppedCreditor.TotalAmountTaken + debt });
            break;

          case > 0:
            pendingTransactions.Add(new PendingTransaction
            {
              SenderId = poppedDebtor.Id,
              ReceiverId = poppedCreditor.Id,
              Amount = credit,
              IsoCode = currentIsoCode,
            });

            debtors.Enqueue(poppedDebtor with { TotalAmountGiven = poppedDebtor.TotalAmountGiven + credit });
            break;

          case 0:
            pendingTransactions.Add(new PendingTransaction
            {
              SenderId = poppedDebtor.Id,
              ReceiverId = poppedCreditor.Id,
              Amount = credit, //credit == debt
              IsoCode = currentIsoCode,
            });
            break;
        }
      }
    });

    return pendingTransactions;
  }

  public static void AddAsCreatorAndMember(this Group group, User user)
  {
    group.Members.Add(user);
    group.Creator = user;
  }

  public static Dictionary<string, List<TransactionTimelineItem>> ToTimelineByUserId(this Group group, int userId)
  {
    var uniqueIsoCodeList = group.UniqueCurrencyCodes();
    var transactionTimelineForEachCurrency = new Dictionary<string, List<TransactionTimelineItem>>();

    // Loop currencies used
    foreach (var currencyCode in uniqueIsoCodeList)
    {
      // New list of TransactionMemberStats
      var transactionMemberStatsList = new List<TransactionMemberStats>();

      // Loop all expenses & add to list
      foreach (var expense in group.Expenses.Where(exp => exp.IsoCode == currencyCode))
      {
        var transactionMemberStats = expense.ToTransactionMemberStatsFromUserId(userId);

        if (transactionMemberStats is not null) {
          transactionMemberStatsList.Add(transactionMemberStats);
        }
      };

      // Loop all transfers & add to list
      foreach (var transfer in group.Transfers.Where(exp => exp.IsoCode == currencyCode))
      {
        var transactionMemberStats = transfer.ToTransactionMemberStatsFromUserId(userId);

        if (transactionMemberStats is not null) {
          transactionMemberStatsList.Add(transactionMemberStats);
        }
      };

      // Sort list
      var sortedTransactionMemberStatsList = transactionMemberStatsList.OrderBy(transactionStats => transactionStats.CreatedAt);

      // New empty timeline for current currency & initialize totals
      var transactionTimelineForCurrency = new List<TransactionTimelineItem>();
      var totalLentSoFar = 0m;
      var totalBorrowedSoFar = 0m;

      // Loop sortedTransactionMemberStatsList created before
      foreach (var transactionMemberStats in sortedTransactionMemberStatsList)
      {
        totalLentSoFar += transactionMemberStats.Lent;
        totalBorrowedSoFar += transactionMemberStats.Borrowed;

        transactionTimelineForCurrency.Add(transactionMemberStats.ToTransactionTimelineItem(totalLentSoFar, totalBorrowedSoFar));
      };

      transactionTimelineForEachCurrency.Add(currencyCode, transactionTimelineForCurrency);
    };

    return transactionTimelineForEachCurrency;
  }

}

public record Participant
{

  public Participant(int id, decimal totalAmountGiven, decimal totalAmountTaken)
  {
    Id = id;
    TotalAmountGiven = totalAmountGiven;
    TotalAmountTaken = totalAmountTaken;
  }
  public int Id { get; set; }
  public decimal TotalAmountGiven { get; set; }
  public decimal TotalAmountTaken { get; set; }
}
