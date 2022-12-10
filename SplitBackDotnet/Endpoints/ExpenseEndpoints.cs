using SplitBackDotnet.Data;
using SplitBackDotnet.Models;
using SplitBackDotnet.Dtos;
using AutoMapper;
using SplitBackDotnet.Helper;
using Microsoft.AspNetCore.Authorization;
using SplitBackDotnet.Extensions;

namespace SplitBackDotnet.Endpoints;

public static class ExpenseEndpoints
{
  public static void MapExpenseEndpoints(this IEndpointRouteBuilder app)
  {
    app.MapPost("/addExpense", async (IRepo repo, NewExpenseDto newExpenseDto) =>
    {
      try
      {
        var expenseValidator = new ExpenseValidator();
        var validationResult = expenseValidator.Validate(newExpenseDto);
        if (validationResult.Errors.Count > 0)
        {
          return Results.Ok(validationResult.Errors.Select(x => new
          {
            Message = x.ErrorMessage,
            Field = x.PropertyName
          }));
        }
        ExpenseSetUp.AllocateAmountEqually(newExpenseDto);
        await repo.AddNewExpense(newExpenseDto);

        var group = await repo.GetGroupById(newExpenseDto.GroupId);
        if (group is null) throw new Exception();
        return Results.Ok(group.PendingTransactions());
      }
      catch (Exception ex)
      {
        return Results.BadRequest(ex.Message);
      }
    });

    app.MapPost("/editExpense", async (IRepo repo, NewExpenseDto newExpenseDto) =>
       {
         try
         {
           var expenseValidator = new ExpenseValidator();
           var validationResult = expenseValidator.Validate(newExpenseDto);
           if (validationResult.Errors.Count > 0)
           {
             return Results.Ok(validationResult.Errors.Select(x => new
             {
               Message = x.ErrorMessage,
               Field = x.PropertyName
             }));
           }
           ExpenseSetUp.AllocateAmountEqually(newExpenseDto);
           await repo.EditExpense(newExpenseDto);

           var group = await repo.GetGroupById(newExpenseDto.GroupId);
           if (group is null) throw new Exception();
           return Results.Ok(group.PendingTransactions());
         }
         catch (Exception ex)
         {
           return Results.BadRequest(ex.Message);
         }
       });

    app.MapPost("/removeExpense", async (IRepo repo, RemoveExpenseDto removeExpenseDto) =>
    {
      try
      {
        var group = await repo.GetGroupById(removeExpenseDto.GroupId.ToInt());
        await repo.RemoveExpense(removeExpenseDto);
        if (group is null) throw new Exception();
        return Results.Ok(group.PendingTransactions());
      }
      catch (Exception ex)
      {
        return Results.BadRequest(ex.Message);
      }
    });



    app.MapPost("/txHistory", async (IRepo repo, TransactionHistoryDto txHistoryDto) =>
    {
      try
      {
        var group = await repo.GetGroupById(txHistoryDto.GroupId.ToInt());
        if (group is null) throw new Exception();
        if (group.Expenses.Count == 0)
        {
          return Results.Ok(new List<TransactionTimelineItem>());
        }
        return Results.Ok(group.GetTransactionHistory());
      }
      catch (Exception ex)
      {
        return Results.BadRequest(ex.InnerException);
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