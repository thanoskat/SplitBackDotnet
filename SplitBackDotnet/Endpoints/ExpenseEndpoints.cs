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
      try
      {
        var Group = await repo.GetGroupById(newExpenseDto.GroupId);
        var Currency = context.Currencies.FirstOrDefault(c => c.isoCode == newExpenseDto.Currency.isoCode);
        ExpenseSetUp.AllocateAmountEqually(newExpenseDto);
        await repo.AddNewExpense(Currency!, newExpenseDto, Group!, mapper);
        CalcPending.PendingTransactions(Group?.Expenses!, Group?.Transfers!, Group?.Members!, Group!, repo, context);
      }
      catch
      {
        Console.WriteLine("Expense Error");
      }
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