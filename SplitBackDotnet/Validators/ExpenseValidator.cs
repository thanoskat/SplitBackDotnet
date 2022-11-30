using FluentValidation;
using SplitBackDotnet.Dtos;
using NMoneys;
public class ExpenseValidator : AbstractValidator<NewExpenseDto>
{
  public ExpenseValidator(NewExpenseDto Dto)
  {
    RuleFor(newExpense=>newExpense.isoCode).IsEnumName(typeof(CurrencyIsoCode))
    .WithMessage("Invalid Currency");

    RuleFor(newExpense => newExpense.Amount)
    .GreaterThan(0).WithMessage("Amount must be greater than zero")
    .NotEmpty().WithMessage("Amount is required");

    RuleFor(newExpense => newExpense.Description)
    .NotNull().WithMessage("Description is required");

    RuleFor(newExpense => newExpense.ExpenseParticipants)
    .Must(ep => ep.Sum(p => p.ContributionAmount) == Dto.Amount)
    .WithMessage("Member amounts don\'t add up to total.");

    RuleFor(newExpense => newExpense.ExpenseSpenders)
    .Must(es => es.Sum(s => s.SpenderAmount) == Dto.Amount)
    .WithMessage("Payers\' amounts don\'t add up to total.");
    
    RuleFor(newExpense => newExpense.ExpenseParticipants.Count)
    .GreaterThan(0)
    .WithMessage("At least one member should be selected");

    RuleFor(newExpense => newExpense.ExpenseSpenders.Count)
    .GreaterThan(0)
    .WithMessage("At least one payer should be selected");
  }
}