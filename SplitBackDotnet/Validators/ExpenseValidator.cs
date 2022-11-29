using FluentValidation;
using SplitBackDotnet.Dtos;

public class ExpenseValidator : AbstractValidator<NewExpenseDto>
{
  public ExpenseValidator()
  {
    RuleFor(newExpense => newExpense.Amount).NotNull().GreaterThan(0);
    RuleFor(newExpense => newExpense.Description).NotNull();
    RuleFor(newExpense => newExpense.Amount == newExpense.ExpenseParticipants.Sum(ep => ep.ContributionAmount));
    RuleFor(newExpense => newExpense.Amount == newExpense.ExpenseSpenders.Sum(ep => ep.SpenderAmount));
    RuleFor(newExpense=>newExpense.ExpenseParticipants.Count).GreaterThan(0);
    RuleFor(newExpense=>newExpense.ExpenseSpenders.Count).GreaterThan(0);

  }

//   protected bool ContributionSumEqualsAmount(ICollection<ExpenseParticipantDto> ExpenseParticipants)
//   {
//     decimal TotalAmountCheck = ExpenseParticipants.Sum(ep => ep.ContributionAmount);
//     if (Amount == TotalAmountCheck)
//     {
//       return true;
//     }
//     else
//     {
//       return false;
//     }
//   }
}