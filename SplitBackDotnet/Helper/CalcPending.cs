using SplitBackDotnet.Models;
using System.Linq;

namespace SplitBackDotnet.Helper {

  public class Spender {
    public int Id { get; set; }
    public long Balance { get; set; }
    public long MoneySummedAndDistributed { get; set; }

  }

  public class PendingTransaction {

    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public long Amount { get; set; }
  }

  public class CalcPending {
    public static List<PendingTransaction> PendingTransactions(ICollection<Expense> Expenses, ICollection<Transfer> Transfers, ICollection<User> Members) {

      List<Spender>[] Spenders = new List<Spender>[((ulong)Members.Count)];

      List<PendingTransaction> dummyreturn = new List<PendingTransaction>();
      Console.WriteLine(Spenders);

      return dummyreturn;
    }
  }
}