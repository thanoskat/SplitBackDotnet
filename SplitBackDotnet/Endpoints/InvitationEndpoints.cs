using SplitBackDotnet.Data;
using SplitBackDotnet.Dtos;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using SplitBackDotnet.Models;
using MongoDB.Bson;
using Microsoft.AspNetCore.Authorization;
using SplitBackDotnet.Extensions;
namespace SplitBackDotnet.Endpoints;
public static class InvitationEndpoints
{
  public static void MapInvitationEndpoints(this IEndpointRouteBuilder app)
  {
    app.MapPost("/create", async (IRepo repo, InvitationDto invitationDto, IOptions<AlphaSplitDatabaseSettings> alphaSplitDatabaseSettings) =>
    {
      var client = new MongoClient(alphaSplitDatabaseSettings.Value.ConnectionString);
      using var session = await client.StartSessionAsync();
      session.StartTransaction();
      try
      {
        var inviterID = ObjectId.Parse(invitationDto.InviterId);
        var groupID = ObjectId.Parse(invitationDto.GroupId);
        var invitationFound = await repo.GetInvitationByInviter(inviterID, groupID);
        if (invitationFound is null)
        {
          await repo.CreateInvitation(inviterID, groupID);
          await session.CommitTransactionAsync();
          return Results.Ok();
        }
        else
        {
          await session.CommitTransactionAsync();
          return Results.BadRequest($"Invitation for group with id {groupID} already exists");
        }

      }
      catch (Exception ex)
      {
        await session.AbortTransactionAsync();
        return Results.BadRequest(ex.Message);
      }
    });

    app.MapPost("/regenerate", async (IRepo repo, InvitationDto invitationDto, IOptions<AlphaSplitDatabaseSettings> alphaSplitDatabaseSettings) =>
        {
          var client = new MongoClient(alphaSplitDatabaseSettings.Value.ConnectionString);
          using var session = await client.StartSessionAsync();
          session.StartTransaction();
          try
          {
            var inviterID = ObjectId.Parse(invitationDto.InviterId);
            var groupID = ObjectId.Parse(invitationDto.GroupId);
            await repo.DeleteInvitation(inviterID, groupID);
            await repo.CreateInvitation(inviterID, groupID);
            await session.CommitTransactionAsync();
            return Results.Ok();

          }
          catch (Exception ex)
          {
            await session.AbortTransactionAsync();
            return Results.BadRequest(ex.Message);
          }
        });

    app.MapPost("/verify", [Authorize] async (HttpContext httpContext, IRepo repo, VerifyInvitationDto dto, IOptions<AlphaSplitDatabaseSettings> alphaSplitDatabaseSettings) =>
    {
      var authedUserId = httpContext.GetAuthorizedUserId();
      var client = new MongoClient(alphaSplitDatabaseSettings.Value.ConnectionString);
      using var session = await client.StartSessionAsync();
      session.StartTransaction();
      try
      {
        var invitation = repo.GetInvitationByCode(dto.Code).Result;
        if (invitation is null) return Results.BadRequest("Invitation not found");

        var groupDoc = repo.CheckIfUserInGroupMembers(authedUserId,invitation.GroupId).Result;
        if (groupDoc is null) return Results.BadRequest("Member already exists in group");

        var inviter = repo.GetUserById(invitation.Inviter).Result;
        var userDoc = repo.CheckIfGroupInUser(authedUserId,invitation.GroupId).Result;
        if (userDoc is null) return Results.BadRequest("Group already exists in user");

        await session.CommitTransactionAsync();
        return Results.Ok(new
        {
          Message = "Invitation is valid",
          InviterNickName = inviter.Nickname,
          group = groupDoc.Title
        });
      }
      catch (Exception ex)
      {
        //await session.AbortTransactionAsync();
        return Results.BadRequest(ex.Message);
      }
    });
    app.MapPost("/accept", [Authorize] async (HttpContext httpContext, IRepo repo, VerifyInvitationDto dto, IOptions<AlphaSplitDatabaseSettings> alphaSplitDatabaseSettings) =>
    {
      var authedUserId = httpContext.GetAuthorizedUserId();
      var client = new MongoClient(alphaSplitDatabaseSettings.Value.ConnectionString);
      using var session = await client.StartSessionAsync();
      session.StartTransaction();
      try
      {
        var invitation = repo.GetInvitationByCode(dto.Code).Result;
        if (invitation is null) return Results.BadRequest("Invitation not found");

        var groupDoc = repo.CheckAndAddUserInGroupMembers(authedUserId,invitation.GroupId).Result;
        if (groupDoc is null) return Results.BadRequest("Member already exists in group");

        var inviter = repo.GetUserById(invitation.Inviter).Result;
        var userDoc = repo.CheckAndAddGroupInUser(authedUserId,invitation.GroupId).Result;
        if (userDoc is null) return Results.BadRequest("Group already exists in user");

        await session.CommitTransactionAsync();
        return Results.Ok(new
        {
          Message = "User joined group",
          group = groupDoc.Title
        });
      }
      catch (Exception ex)
      {
        await session.AbortTransactionAsync();
        return Results.BadRequest(ex.Message);
      }
    });
  }
}