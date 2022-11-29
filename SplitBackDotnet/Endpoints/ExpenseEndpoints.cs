using Microsoft.EntityFrameworkCore;
using SplitBackDotnet.Data;
using SplitBackDotnet.Models;
using SplitBackDotnet.Dtos;
using AutoMapper;
using SplitBackDotnet.Helper;
using Microsoft.AspNetCore.Authorization;
using NMoneys;
using NMoneys.Allocations;
using NMoneys.Extensions;

namespace SplitBackDotnet.Endpoints;

public static class ExpenseEndpoints
{
  public static void MapExpenseEndpoints(this IEndpointRouteBuilder app)
  {
    app.MapPost("/creategroup", [Authorize] async (HttpContext httpContext, IRepo repo, IMapper mapper, DataContext context, CreateGroupDto createGroupDto) =>
    {
      var group = mapper.Map<Group>(createGroupDto);
      PreGroupSetUp.AddCreatorToMembers(context, httpContext, group);
      await repo.CreateGroup(group);
      await repo.SaveChangesAsync();
    });

    app.MapPost("/addExpense", async (IRepo repo, IMapper mapper, DataContext context, NewExpenseDto newExpenseDto) =>
    {
      var Group = await repo.GetGroupById(newExpenseDto.GroupId);
      if (context.Currencies.Any(c => c.isoCode == newExpenseDto.Currency.isoCode))
      {
        var Currency = context.Currencies.FirstOrDefault(c => c.isoCode == newExpenseDto.Currency.isoCode);
        Expense Expense = new Expense();
        Expense.ExpenseParticipants = mapper.Map<ICollection<ExpenseParticipant>>(newExpenseDto.ExpenseParticipants);
        Expense.ExpenseSpenders = mapper.Map<ICollection<ExpenseSpender>>(newExpenseDto.ExpenseSpenders);
        Expense.Currency = Currency;
        Expense.Amount = newExpenseDto.Amount;
        Expense.Description = newExpenseDto.Description;
        Group?.Expenses?.Add(Expense);
        await repo.SaveChangesAsync();
      }
      else
      {
        var newExpense = mapper.Map<Expense>(newExpenseDto);
        //context.Currencies.Attach(mapper.Map<Models.Currency>(newExpenseDto.Currency));
        Group?.Expenses?.Add(newExpense);
        await repo.SaveChangesAsync();
      }
      CalcPending.PendingTransactions(Group.Expenses, Group.Transfers, Group.Members, Group, repo, context);
    });
  }
}

// var x = newExpenseDto.Amount.Inr().ToString();
// var y = new Money(newExpenseDto.Amount,NMoneys.Currency.Code.Parse("EUR")).ToString();
// Allocation fair = newExpenseDto.Amount.Inr().Allocate(newExpenseDto.ExpenseParticipants.Count); //40m.Eur().Allocate(4);
// var List  = newExpenseDto.Amount.Xxx().Allocate(newExpenseDto.ExpenseParticipants.Count).ToList();
// foreach(Money money in fair ){
//   var f = money.Amount;
// }