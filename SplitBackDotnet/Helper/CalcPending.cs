using SplitBackDotnet.Models;
using SplitBackDotnet.Data;
using Microsoft.EntityFrameworkCore;


namespace SplitBackDotnet.Helper
{
  public class Spender : ISpender
  {
    public Spender(int _Id, long _Balance, long _MoneySummedAndDistributed)
    {
      Id = _Id;
      Balance = _Balance;
      MoneySummedAndDistributed = _MoneySummedAndDistributed;
    }
    public int Id { get; set; }
    public decimal Balance { get; set; }
    public decimal MoneySummedAndDistributed { get; set; }
  }

  public class Debtor : ISpender
  {
    public int Id { get; set; }
    public decimal Balance { get; set; }
  }
  public class Creditor : ISpender
  {
    public int Id { get; set; }
    public decimal Balance { get; set; }
  }

  public class CalcPending
  {
    public static async void PendingTransactions(ICollection<Expense> Expenses, ICollection<Transfer> Transfers, ICollection<User> Members, Group Group, IRepo repo, DataContext context)
    {
      // List<Spender> Spenders1 = new List<Spender>(Members.Count);
      // Separate spenders in debtors and creditors
      List<Debtor> Debtors = new List<Debtor>();
      List<Creditor> Creditors = new List<Creditor>();
      Spender[] Spenders = new Spender[Members.Count];
      //https://stackoverflow.com/questions/202813/adding-values-to-a-c-sharp-array
      for (int m = 0; m < Members.Count; m++)
      {
        Spenders[m] = new Spender(Members.ElementAt(m).UserId, 0, 0);
      }
      // Initialize total amount spent outside of group
      decimal TotalSpent = 0;
      // Loop through expenses
      for (int e = 0; e < Expenses.Count; e++)
      {
        var Amount = Expenses.ElementAt(e).Amount;
        TotalSpent += Amount;
        decimal TotalAmountCheck = Expenses
        .ElementAt(e)
        .ExpenseParticipants.Where(ep => ep.ExpenseId == Expenses.ElementAt(e).ExpenseId)
        .Sum(ep => ep.ContributionAmount);
        if (TotalAmountCheck != Amount) throw new ArgumentException($"{nameof(Amount)} is not equal to {nameof(TotalAmountCheck)}");

        for (int spndr = 0; spndr < Spenders.Length; spndr++)
        {
          Spenders[spndr].MoneySummedAndDistributed += Expenses
          .ElementAt(e)
          .ExpenseParticipants.Where(ep => ep.ParticipantId == Spenders[spndr].Id)
          .Sum(ep => ep.ContributionAmount);

          Spenders[spndr].Balance += (-1) * Expenses
          .ElementAt(e)
          .ExpenseSpenders.Where(es => es.SpenderId == Spenders[spndr].Id)
          .Sum(es => es.SpenderAmount) +
          (-1) * Transfers
          .Where(tr => tr.SenderId == Spenders[spndr].Id)
          .Sum(transfer => transfer.Amount) +
          Transfers
          .Where(tr => tr.ReceiverId == Spenders[spndr].Id)
          .Sum(transfer => transfer.Amount);
        }
      }

      for (int spndr = 0; spndr < Spenders.Length; spndr++)
      {
        decimal DebtOrCredit = Spenders[spndr].Balance + Spenders[spndr].MoneySummedAndDistributed;
        if (DebtOrCredit > 0)
        {
          Debtors.Add(new Debtor { Id = Spenders[spndr].Id, Balance = DebtOrCredit });
        }
        else if (DebtOrCredit < 0)
        {
          Creditors.Add(new Creditor { Id = Spenders[spndr].Id, Balance = DebtOrCredit });
        }
      }

      var NewPendingTransactions = new List<PendingTransaction>();

      while (Debtors.Count > 0 && Creditors.Count > 0)
      {
        Debtor PoppedDebtor = Debtors.ElementAt(Debtors.Count - 1);
        Debtors.RemoveAt(Debtors.Count - 1);
        Creditor PoppedCreditor = Creditors.ElementAt(Creditors.Count - 1);
        Creditors.RemoveAt(Creditors.Count - 1);
        decimal Diff = PoppedDebtor.Balance + PoppedCreditor.Balance;
        decimal AmountPaid = 0;

        if (Diff < 0)
        {
          Creditors.Add(new Creditor { Id = PoppedCreditor.Id, Balance = Diff });
          AmountPaid = Math.Abs(PoppedDebtor.Balance);
        }
        else if (Diff == 0)
        {
          AmountPaid = Math.Abs(PoppedDebtor.Balance);
        }
        else if (Diff > 0)
        {
          Debtors.Add(new Debtor { Id = PoppedDebtor.Id, Balance = Diff });
          AmountPaid = Math.Abs(PoppedCreditor.Balance);
        }
        NewPendingTransactions.Add(new PendingTransaction { SenderId = PoppedDebtor.Id, ReceiverId = PoppedCreditor.Id, Amount = AmountPaid, CurrentGroupId = Group.GroupId, Group=Group });
      }
      Group.PendingTransactions = NewPendingTransactions;
      await repo.SaveChangesAsync();
      //return PendingTransactions;
    }
  }
}

// for (int spndr = 0; spndr < Spenders.Length; spndr++)
// {
//   Spenders[spndr].Balance = Transfers
//   .Where(tr => tr.ReceiverId == Spenders[spndr].Id)
//   .Sum(transfer => transfer.Amount);

//   Spenders[spndr].Balance = (-1) * Transfers
//   .Where(tr => tr.SenderId == Spenders[spndr].Id)
//   .Sum(transfer => transfer.Amount);
// }