using SplitBackDotnet.Data;
using SplitBackDotnet.Models;
using SplitBackDotnet.Dtos;
using AutoMapper;
using SplitBackDotnet.Helper;
using Microsoft.AspNetCore.Authorization;
using SplitBackDotnet.Extensions;
using Raven.Client.Documents.Session;

namespace SplitBackDotnet.Endpoints;

public static class GroupEndpoints
{
  public static void MapGroupEndpoints(this IEndpointRouteBuilder app)
  {
    // app.MapPost("/creategroup", [Authorize] async (HttpContext httpContext, IRepo repo, IMapper mapper, DataContext context, CreateGroupDto createGroupDto) =>
    // {
    //   try
    //   {
    //     var authedUserId = httpContext.GetAuthorizedUserId();
    //     var group = mapper.Map<Group>(createGroupDto);

    //     var newCreator = new User { UserId = authedUserId };
    //     context.Attach<User>(newCreator);

    //     group.AddAsCreatorAndMember(newCreator);
    //     await repo.CreateGroup(group);
    //     await repo.SaveChangesAsync();

    //     return Results.Ok();
    //   }
    //   catch (Exception ex)
    //   {
    //     return Results.BadRequest(ex.Message);
    //   }
    // });

    app.MapPost("/creategroup2", async (HttpContext httpContext,IRepo repo, CreateGroupDto createGroupDto, IMapper mapper) =>
    {
      try
      {
        //var authedUserId = httpContext.GetAuthorizedUserId();
        var group = mapper.Map<Group>(createGroupDto);
        await repo.CreateGroup(group);
        return Results.Ok();
      }
      catch (Exception ex)
      {
        return Results.BadRequest(ex.Message);
      }
    });
  }
}