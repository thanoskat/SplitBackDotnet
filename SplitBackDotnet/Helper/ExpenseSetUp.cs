using SplitBackDotnet.Dtos;
using NMoneys.Allocations;
using NMoneys.Extensions;
using SplitBackDotnet.Models;

namespace SplitBackDotnet.Helper
{
  public static class ExpenseSetUp
  {
    public static void AllocateAmountEqually(NewExpenseDto newExpenseDto)
    {
      if (newExpenseDto.SplitEqually == true)
      {
        var DistributedAmountArr = newExpenseDto.Amount.Xxx().Allocate(newExpenseDto.ExpenseParticipants.Count).ToList();
        int index = 0;
        foreach (ExpenseParticipantDto Participant in newExpenseDto.ExpenseParticipants)
        {
          Participant.ContributionAmount = DistributedAmountArr[index].Amount;
          index = index + 1;
        }
      }
    }

    public static void CheckTotalAmountVsTotalContributions(ICollection<Expense> Expenses, decimal TotalSpent, int e)
    {
      var Amount = Expenses.ElementAt(e).Amount;
      TotalSpent += Amount;
      decimal TotalAmountCheck = Expenses
      .ElementAt(e)
      .ExpenseParticipants.Where(ep => ep.ExpenseId == Expenses.ElementAt(e).ExpenseId)
      .Sum(ep => ep.ContributionAmount);
      if (TotalAmountCheck != Amount) throw new ArgumentException($"{nameof(Amount)} is not equal to {nameof(TotalAmountCheck)}");
    }
  }
}