using SplitBackDotnet.Models;
using SplitBackDotnet.Helper;

namespace SplitBackDotnet.Extensions;

public static class GroupExtensions
{
  public static IEnumerable<string> UniqueIsoCodes(this Group group)
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
  
  // public static HistoricalTransaction ToHistoricalTransaction(this ITransaction transaction, decimal lent, decimal borrowed, decimal userPaid, decimal userShare) {
    
  //   return new HistoricalTransaction {
  //     TransactionId = transaction.TransactionId,
  //     CreatedAt = transaction.CreatedAt,
  //     Description = transaction.Description,
  //     Lent = lent,
  //     Borrowed = 0,
  //     UserPaid = userPaid,
  //     UserShare = userShare,
  //     IsTransfer = transaction is Transfer,
  //     IsoCode = transaction.IsoCode
  //   };
  // }
  
  public static HistoricalTransaction? ToHistoricalTransactionFromUserId(this Expense expense, int userId) {
    
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
    
    return new HistoricalTransaction {
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
  
  public static HistoricalTransaction? ToHistoricalTransactionFromUserId(this Transfer transfer, int userId) {
    
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
    
    return new HistoricalTransaction {
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

  public static List<HistoricalTransaction> GetTransactionHistory(this Group group)
  {    
    var userId = 3;
    var history = new List<HistoricalTransaction>();
    var uniqueIsoCodeList = group.UniqueIsoCodes();

    uniqueIsoCodeList.ToList().ForEach(currentIsoCode => {
      
      group.Expenses.Where(exp => exp.IsoCode == currentIsoCode).ToList().ForEach(expense => {
        
        var historicalTransaction = expense.ToHistoricalTransactionFromUserId(userId);
          
        if (historicalTransaction is not null) {
          history.Add(historicalTransaction);
        }
      });
      
      group.Transfers.Where(exp => exp.IsoCode == currentIsoCode).ToList().ForEach(transfer => {
          
        var historicalTransaction = transfer.ToHistoricalTransactionFromUserId(userId);
          
        if (historicalTransaction is not null) {
          history.Add(historicalTransaction);
        }
      });
      
      var sortedHistoricalTransactions = history.OrderBy(h => h.CreatedAt).ToList();
      
      // sortedHistoricalTransactions.Select
      
    });
        // if (isSpender)
        // {
        //   var spenderAmount = exp.ExpenseSpenders.ToList().Single(es => es.SpenderId == userId).SpenderAmount;
        //   bool isParticipant = exp.ExpenseParticipants.ToList().Any(ep => ep.ParticipantId == userId);
        //   if (isParticipant)
        //   {
        //     var participantAmount = exp.ExpenseParticipants.ToList().Single(ep => ep.ParticipantId == userId).ContributionAmount;
        //     history.Add(new HistoricalTransaction
        //     {
        //       ExpenseId = exp.ExpenseId,
        //       CreatedAt = exp.CreatedAt,
        //       Description = exp.Description,
        //       Lent = spenderAmount - participantAmount,
        //       Borrowed = 0,
        //       UserPaid = spenderAmount,
        //       UserShare = participantAmount,
        //       IsTransfer = false,
        //       IsoCode = exp.IsoCode

        //     });
        //   }
        //   else
        //   {
        //     history.Add(new HistoricalTransaction
        //     {
        //       ExpenseId = exp.ExpenseId,
        //       CreatedAt = exp.CreatedAt,
        //       Description = exp.Description,
        //       Lent = spenderAmount,
        //       Borrowed = 0,
        //       UserPaid = spenderAmount,
        //       UserShare = 0,
        //       IsTransfer = false,
        //       IsoCode = exp.IsoCode
        //     });
        //   }
        // }
        // else
        // {
        //   bool isParticipant = exp.ExpenseParticipants.ToList().Any(ep => ep.ParticipantId == userId);
        //   if (isParticipant)
        //   {
        //     var participantAmount = exp.ExpenseParticipants.ToList().Single(ep => ep.ParticipantId == userId).ContributionAmount;
        //     history.Add(new HistoricalTransaction
        //     {
        //       ExpenseId = exp.ExpenseId,
        //       CreatedAt = exp.CreatedAt,
        //       Description = exp.Description,
        //       Lent = 0,
        //       Borrowed = participantAmount,
        //       UserPaid = 0,
        //       UserShare = 0,
        //       IsTransfer = false,
        //       IsoCode = exp.IsoCode
        //     });
        //   }
        // }
      // });

      // group.Transfers.Where(exp => exp.IsoCode == currentIsoCode).ToList().ForEach(tr =>
      // {
      //   if (tr.SenderId == userId)
      //   {
      //     history.Add(new HistoricalTransaction
      //     {
      //       TransactionId = tr.TransferId,
      //       CreatedAt = tr.CreatedAt,
      //       Description = tr.Description,
      //       Lent = tr.Amount,
      //       Borrowed = 0,
      //       UserPaid = 0,
      //       UserShare = 0,
      //       IsTransfer = true,
      //       IsoCode = tr.IsoCode
      //     });
      //   }
      //   else if (tr.ReceiverId == userId)
      //   {
      //     history.Add(new HistoricalTransaction
      //     {
      //       TransactionId = tr.TransferId,
      //       CreatedAt = tr.CreatedAt,
      //       Description = tr.Description,
      //       Lent = 0,
      //       Borrowed = tr.Amount,
      //       UserPaid = 0,
      //       UserShare = 0,
      //       IsTransfer = true,
      //       IsoCode = tr.IsoCode
      //     });
      //   }
      // });
    // });

    var sortedHistory = history.OrderBy(h => h.CreatedAt).ToList();

    var sortedHistoryByIsoCode = sortedHistory.GroupBy(sh => sh.IsoCode);

    sortedHistoryByIsoCode.ToList().ForEach(txEntryByIsoCode =>
    {
      var txEntryByIsoCodeList = txEntryByIsoCode.ToList();
      if (txEntryByIsoCodeList.Count == 1)
      {
        txEntryByIsoCodeList[0].TotalLent = txEntryByIsoCodeList[0].Lent;
        txEntryByIsoCodeList[0].TotalBorrowed = txEntryByIsoCodeList[0].Borrowed;
        txEntryByIsoCodeList[0].Balance = txEntryByIsoCodeList[0].Lent - txEntryByIsoCodeList[0].Borrowed;
      }
      else
      {
        txEntryByIsoCodeList[0].TotalLent = txEntryByIsoCodeList[0].Lent;
        txEntryByIsoCodeList[0].TotalBorrowed = txEntryByIsoCodeList[0].Borrowed;
        txEntryByIsoCodeList[0].Balance = txEntryByIsoCodeList[0].Lent - txEntryByIsoCodeList[0].Borrowed;

        txEntryByIsoCodeList[1].TotalLent = txEntryByIsoCodeList[0].Lent + txEntryByIsoCodeList[1].Lent;
        txEntryByIsoCodeList[1].TotalBorrowed = txEntryByIsoCodeList[0].Borrowed + txEntryByIsoCodeList[1].Borrowed;
        txEntryByIsoCodeList[1].Balance = txEntryByIsoCodeList[1].Lent - txEntryByIsoCodeList[1].Borrowed;

        for (int i = 1; i < txEntryByIsoCodeList.Count; i++)
        {
          txEntryByIsoCodeList[i].TotalLent = txEntryByIsoCodeList[i - 1].TotalLent + txEntryByIsoCodeList[i].Lent;
          txEntryByIsoCodeList[i].TotalBorrowed = txEntryByIsoCodeList[i - 1].TotalBorrowed + txEntryByIsoCodeList[i].Borrowed;
          txEntryByIsoCodeList[i].Balance = txEntryByIsoCodeList[i].TotalLent - txEntryByIsoCodeList[i].TotalBorrowed;
        }
      }
    });

    // if (shortedHistory.Count == 1)
    // {
    //   shortedHistory[0].TotalLent = shortedHistory[0].Lent;
    //   shortedHistory[0].TotalBorrowed = shortedHistory[0].Borrowed;
    //   shortedHistory[0].Balance = shortedHistory[0].Lent - shortedHistory[0].Borrowed;
    // }
    // else
    // {
    //   shortedHistory[0].TotalLent = shortedHistory[0].Lent;
    //   shortedHistory[0].TotalBorrowed = shortedHistory[0].Borrowed;
    //   shortedHistory[0].Balance = shortedHistory[0].Lent - shortedHistory[0].Borrowed;

    //   shortedHistory[1].TotalLent = shortedHistory[0].Lent + shortedHistory[1].Lent;
    //   shortedHistory[1].TotalBorrowed = shortedHistory[0].Borrowed + shortedHistory[1].Borrowed;
    //   shortedHistory[1].Balance = shortedHistory[1].Lent - shortedHistory[1].Borrowed;

    //   for (int i = 2; i < shortedHistory.Count; i++)
    //   {
    //     shortedHistory[i].TotalLent = shortedHistory[i - 1].TotalLent + shortedHistory[i].Lent;
    //     shortedHistory[i].TotalBorrowed = shortedHistory[i - 1].TotalBorrowed + shortedHistory[i].Borrowed;
    //     shortedHistory[i].Balance = shortedHistory[i].TotalLent - shortedHistory[i].TotalBorrowed;
    //   }
    // }

    return sortedHistory;
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


