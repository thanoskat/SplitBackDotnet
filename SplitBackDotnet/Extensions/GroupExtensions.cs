using SplitBackDotnet.Models;
using SplitBackDotnet.Data;

namespace SplitBackDotnet.Extensions;

public static class GroupExtensions
{

  public static List<PendingTransaction> PendingTransactions(this Group group)
  {

    var expenseListsByIsoCode = group.Expenses.GroupBy(exp => exp.IsoCode);
    var transferListsByIsoCode = group.Transfers.GroupBy(tr => tr.IsoCode);

    var isoCodeList = new List<string>();
    expenseListsByIsoCode.ToList().ForEach(list => isoCodeList.Add(list.Key));
    transferListsByIsoCode.ToList().ForEach(list => isoCodeList.Add(list.Key));
    var uniqueIsoCodeList = isoCodeList.Distinct();

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

      group.Transfers.ToList().ForEach(transfer =>
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



  public static List<HistoricalTransaction> GetTransactionHistory(this Group group)
  {
    var userId = 4;
    var history = new List<HistoricalTransaction>();
    group.Expenses.ToList().ForEach(exp =>
    {
      bool isSpender = exp.ExpenseSpenders.ToList().Any(es => es.SpenderId == userId);
      if (isSpender)
      {
        var spenderAmount = exp.ExpenseSpenders.ToList().Single(es => es.SpenderId == userId).SpenderAmount;
        bool isParticipant = exp.ExpenseParticipants.ToList().Any(ep => ep.ParticipantId == userId);
        if (isParticipant)
        {
          var participantAmount = exp.ExpenseParticipants.ToList().Single(ep => ep.ParticipantId == userId).ContributionAmount;
          history.Add(new HistoricalTransaction
          {
            ExpenseId = exp.ExpenseId,
            CreatedAt = exp.CreatedAt,
            Description = exp.Description,
            Lent = spenderAmount - participantAmount,
            Borrowed = 0,
            UserPaid = spenderAmount,
            UserShare = participantAmount,
            IsTransfer = false
          });
        }
        else
        {
          history.Add(new HistoricalTransaction
          {
            ExpenseId = exp.ExpenseId,
            CreatedAt = exp.CreatedAt,
            Description = exp.Description,
            Lent = spenderAmount,
            Borrowed = 0,
            UserPaid = spenderAmount,
            UserShare = 0,
            IsTransfer = false
          });
        }
      }
      else
      {
        bool isParticipant = exp.ExpenseParticipants.ToList().Any(ep => ep.ParticipantId == userId);
        if (isParticipant)
        {
          var participantAmount = exp.ExpenseParticipants.ToList().Single(ep => ep.ParticipantId == userId).ContributionAmount;
          history.Add(new HistoricalTransaction
          {
            ExpenseId = exp.ExpenseId,
            CreatedAt = exp.CreatedAt,
            Description = exp.Description,
            Lent = 0,
            Borrowed = participantAmount,
            UserPaid = 0,
            UserShare = 0,
            IsTransfer = false
          });
        }
      }
    });

    group.Transfers.ToList().ForEach(tr =>
    {
      if (tr.SenderId == userId)
      {
        history.Add(new HistoricalTransaction
        {
          ExpenseId = tr.TransferId,
          CreatedAt = tr.CreatedAt,
          Description = tr.Description,
          Lent = tr.Amount,
          Borrowed = 0,
          UserPaid = 0,
          UserShare = 0,
          IsTransfer = true
        });
      }
      else if (tr.ReceiverId == userId)
      {
        history.Add(new HistoricalTransaction
        {
          ExpenseId = tr.TransferId,
          CreatedAt = tr.CreatedAt,
          Description = tr.Description,
          Lent = 0,
          Borrowed = tr.Amount,
          UserPaid = 0,
          UserShare = 0,
          IsTransfer = true
        });
      }
    });

    var shortedHistory = history.OrderBy(h => h.CreatedAt).ToList();

    if (shortedHistory.Count == 1)
    {
      shortedHistory[0].TotalLent = shortedHistory[0].Lent;
      shortedHistory[0].TotalBorrowed = shortedHistory[0].Borrowed;
      shortedHistory[0].Balance = shortedHistory[0].Lent - shortedHistory[0].Borrowed;
    }
    else
    {
      shortedHistory[0].TotalLent = shortedHistory[0].Lent;
      shortedHistory[0].TotalBorrowed = shortedHistory[0].Borrowed;
      shortedHistory[0].Balance = shortedHistory[0].Lent - shortedHistory[0].Borrowed;

      shortedHistory[1].TotalLent = shortedHistory[0].Lent + shortedHistory[1].Lent;
      shortedHistory[1].TotalBorrowed = shortedHistory[0].Borrowed + shortedHistory[1].Borrowed;
      shortedHistory[1].Balance = shortedHistory[1].Lent - shortedHistory[1].Borrowed;

      for (int i = 2; i < shortedHistory.Count; i++)
      {
        shortedHistory[i].TotalLent = shortedHistory[i - 1].TotalLent + shortedHistory[i].Lent;
        shortedHistory[i].TotalBorrowed = shortedHistory[i - 1].TotalBorrowed + shortedHistory[i].Borrowed;
        shortedHistory[i].Balance = shortedHistory[i].TotalLent - shortedHistory[i].TotalBorrowed;
      }
    }

    return shortedHistory;
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


