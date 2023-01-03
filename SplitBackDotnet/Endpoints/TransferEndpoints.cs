using SplitBackDotnet.Data;
using SplitBackDotnet.Dtos;
using SplitBackDotnet.Extensions;
using MongoDB.Bson;
namespace SplitBackDotnet.Endpoints;

public static class TransferEndpoints
{
  public static void MapTransferEndpoints(this IEndpointRouteBuilder app)
  {
    app.MapPost("/addTransfer", async (IRepo repo, NewTransferDto newTransferDto) =>
    {
      try
      {
        var groupId = ObjectId.Parse(newTransferDto.GroupId);
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
        var group = await repo.GetGroupById( groupId);
        if (group is null) throw new Exception();
        return Results.Ok(group.PendingTransactions());
      }
      catch (Exception ex)
      {
        return Results.BadRequest(ex.Message);
      }
    });

    app.MapPost("/editTransfer", async (IRepo repo, EditTransferDto editTransferDto) =>
    {
      try
      {
        var groupId = ObjectId.Parse(editTransferDto.GroupId);
        var transferValidator = new TransferValidator();
        var validationResult = transferValidator.Validate(editTransferDto);
        if (validationResult.Errors.Count > 0)
        {
          return Results.Ok(validationResult.Errors.Select(x => new
          {
            Message = x.ErrorMessage,
            Field = x.PropertyName
          }));
        }
        await repo.EditTransfer(editTransferDto);
        var group = await repo.GetGroupById(groupId);
        if (group is null) throw new Exception();
        return Results.Ok(group.PendingTransactions());
      }
      catch (Exception ex)
      {
        return Results.BadRequest(ex.Message);
      }
    });
    
    app.MapPost("/removeOrRestoreTransfer", async (IRepo repo, RemoveRestoreTransferDto removeRestoreTransferDto) =>
    {var groupId = ObjectId.Parse(removeRestoreTransferDto.GroupId);
      try
      {//need a transaction here?
        await repo.RemoveOrRestoreTransfer(removeRestoreTransferDto);
        var group = await repo.GetGroupById(groupId);
        if (group is null) throw new Exception();
        return Results.Ok(group.PendingTransactions());
      }
      catch (Exception ex)
      {
        return Results.BadRequest(ex.InnerException);
      }
    });
  }
}