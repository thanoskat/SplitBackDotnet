using SplitBackDotnet.Dtos;
using NMoneys.Allocations;
using NMoneys.Extensions;
using SplitBackDotnet.Models;
using SplitBackDotnet.Extentions;
using NMoneys;

namespace SplitBackDotnet.Helper
{
  public static class ExpenseSetUp
  {
    public static void AllocateAmountEqually(NewExpenseDto newExpenseDto)
    {
      if (newExpenseDto.SplitEqually == true)
      {
        bool success = Enum.TryParse<CurrencyIsoCode>(newExpenseDto.IsoCode, out CurrencyIsoCode isoCode);
        if (!success)
        {
          throw new Exception();
        }
        var money = new Money(newExpenseDto.Amount.ToDecimal(), isoCode);
        var DistributedAmountArr = money.Allocate(newExpenseDto.ExpenseParticipants.Count).ToList();
        int index = 0;
        foreach (ExpenseParticipantDto Participant in newExpenseDto.ExpenseParticipants)
        {
          Participant.ContributionAmount = DistributedAmountArr[index].Amount.ToString();
          index = index + 1;
        }
      }
    }
  }
}