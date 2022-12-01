using FluentValidation;
using SplitBackDotnet.Dtos;
using SplitBackDotnet.Extentions;
using NMoneys;
public class ExpenseValidator : AbstractValidator<NewExpenseDto>
{
  public ExpenseValidator()
  {
    RuleFor(newExpense => newExpense.IsoCode).IsEnumName(typeof(CurrencyIsoCode))
    .WithMessage("Invalid Currency");

    RuleFor(newExpense => newExpense.Amount)
    .Must(x => decimal.TryParse(x, out var val))
    .WithMessage("Amount must be a number.")
    .DependentRules(() =>
    {
      RuleFor(newExpense => newExpense.Description)
      .NotNull().NotEmpty().WithMessage("Description is required");

      When(newExpense => !newExpense.SplitEqually && newExpense.ExpenseParticipants.Count >= 2, () =>
   {
     RuleFor(newExpense => newExpense)
     .Must(ne => ne.ExpenseParticipants.
     Sum(ep => ep.ContributionAmount) == ne.Amount.ToDecimal())
     .WithMessage("Member amounts don\'t add up to total.");
   });

      When(newExpense => newExpense.ExpenseSpenders.Count >= 2, () =>
    {
      RuleFor(newExpense => newExpense)
      .Must(ne => ne.ExpenseSpenders
      .Sum(es => es.SpenderAmount) == ne.Amount.ToDecimal())
     .WithMessage("Payers\' amounts don\'t add up to total.");
    });

      RuleFor(newExpense => newExpense.ExpenseParticipants.Count)
      .GreaterThan(0)
      .WithMessage("At least one member should be selected");

      RuleFor(newExpense => newExpense.ExpenseSpenders.Count)
      .GreaterThan(0)
      .WithMessage("At least one payer should be selected");
    });
  }
}