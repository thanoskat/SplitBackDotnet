using SplitBackDotnet.Models;
using SplitBackDotnet.Data;

namespace SplitBackDotnet.Helper {

  public static class GroupExtensions {

    public static List<PendingTransaction> PendingTransactions(this Group group, IRepo repo, DataContext context) {

      var expenseListsByIsoCode = group.Expenses.GroupBy(exp => exp.IsoCode);
      var transferListsByIsoCode = group.Transfers.GroupBy(tr => tr.IsoCode);

      var isoCodeList = new List<string>();
      expenseListsByIsoCode.ToList().ForEach(list => isoCodeList.Add(list.Key));
      transferListsByIsoCode.ToList().ForEach(list => isoCodeList.Add(list.Key));
      var uniqueIsoCodeList = isoCodeList.Distinct();

      var pendingTransactions = new List<PendingTransaction>();

      uniqueIsoCodeList.ToList().ForEach(currentIsoCode => {
        var participants = new List<Participant>();

        group.Members.ToList().ForEach(member => {
          participants.Add(new Participant(member.UserId, 0m, 0m));
        });

        group.Expenses.Where(exp => exp.IsoCode == currentIsoCode).ToList().ForEach(expense => {

          expense.ExpenseParticipants.ToList().ForEach(expenseParticipant => {
            participants.Single(p => p.Id == expenseParticipant.ParticipantId).TotalAmountTaken += expenseParticipant.ContributionAmount;
          });

          expense.ExpenseSpenders.ToList().ForEach(expenseSpender => {
            participants.Single(p => p.Id == expenseSpender.SpenderId).TotalAmountGiven += expenseSpender.SpenderAmount;
          });
        });

        group.Transfers.ToList().ForEach(transfer => {

          participants.Single(p => p.Id == transfer.ReceiverId).TotalAmountTaken += transfer.Amount;

          participants.Single(p => p.Id == transfer.SenderId).TotalAmountGiven += transfer.Amount;
        });

        var debtors = new Queue<Participant>();
        var creditors = new Queue<Participant>();

        participants.ForEach(p => {

          switch(p.TotalAmountGiven - p.TotalAmountTaken) {

            case < 0:
              debtors.Enqueue(p);
              break;

            case > 0:
              creditors.Enqueue(p);
              break;
          }
        });

        while(debtors.Count > 0 && creditors.Count > 0) {

          var poppedDebtor = debtors.Dequeue();
          var poppedCreditor = creditors.Dequeue();

          var debt = (poppedDebtor.TotalAmountTaken - poppedDebtor.TotalAmountGiven);
          var credit = (poppedCreditor.TotalAmountGiven - poppedCreditor.TotalAmountTaken);
          var diff = debt - credit;

          switch(diff) {

            case < 0:
              pendingTransactions.Add(new PendingTransaction {
                SenderId = poppedDebtor.Id,
                ReceiverId = poppedCreditor.Id,
                Amount = debt,
                IsoCode = currentIsoCode,
              });

              creditors.Enqueue(poppedCreditor with { TotalAmountTaken = poppedCreditor.TotalAmountTaken + debt });
              break;

            case > 0:
              pendingTransactions.Add(new PendingTransaction {
                SenderId = poppedDebtor.Id,
                ReceiverId = poppedCreditor.Id,
                Amount = credit,
                IsoCode = currentIsoCode,
              });

              debtors.Enqueue(poppedDebtor with { TotalAmountGiven = poppedDebtor.TotalAmountGiven + credit });
              break;

            case 0:
              pendingTransactions.Add(new PendingTransaction {
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
  }

  public record Participant {

    public Participant(int id, decimal totalAmountGiven, decimal totalAmountTaken) {
      Id = id;
      TotalAmountGiven = totalAmountGiven;
      TotalAmountTaken = totalAmountTaken;
    }

    public int Id { get; set; }

    public decimal TotalAmountGiven { get; set; }

    public decimal TotalAmountTaken { get; set; }
  }
}
