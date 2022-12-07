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
    app.MapPost("/creategroup", [Authorize] async (HttpContext httpContext, IRepo repo, IMapper mapper, DataContext context, CreateGroupDto createGroupDto) =>
    {
      try {
        var authedUserId = httpContext.GetAuthorizedUserId();
        var group = mapper.Map<Group>(createGroupDto);
        
        var newCreator = new User{ UserId = authedUserId };
        context.Attach<User>(newCreator);

        group.AddAsCreatorAndMember(newCreator);

        await repo.CreateGroup(group);
        await repo.SaveChangesAsync();

        return Results.Ok();
      }
      catch(Exception ex) {
        return Results.BadRequest(ex.Message);
      }
    });

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

    app.MapPost("/addTransfer", async (IRepo repo, NewTransferDto newTransferDto) =>
    {
      try
      {
        var transferValidator = new TransferValidator();
        var validationResult = transferValidator.Validate(newTransferDto);
        if (validationResult.Errors.Count > 0)
        {
          return Results.Ok(validationResult.Errors.Select(x => new
          {
            Message = x.ErrorMessage,
            Field = x.PropertyName
          }));
        }
        await repo.AddNewTransfer(newTransferDto);
        var group = await repo.GetGroupById(newTransferDto.GroupId);
        if (group is null) throw new Exception();
        return Results.Ok(group.PendingTransactions());
      }
      catch (Exception ex)
      {
        return Results.BadRequest(ex.Message);
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