using SplitBackDotnet.Data;
using SplitBackDotnet.Dtos;
using SplitBackDotnet.Extensions;
namespace SplitBackDotnet.Endpoints;

public static class TransferEndpoints
{
  public static void MapTransferEndpoints(this IEndpointRouteBuilder app)
  {
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
    
    app.MapPost("/removeTransfer", async (IRepo repo, RemoveTransferDto removeTransferDto) =>
    {
      try
      {
        var group = await repo.GetGroupById(removeTransferDto.GroupId.ToInt());
        await repo.RemoveTransfer(removeTransferDto);
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