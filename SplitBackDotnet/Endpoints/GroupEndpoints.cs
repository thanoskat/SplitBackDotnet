using SplitBackDotnet.Data;
using SplitBackDotnet.Models;
using SplitBackDotnet.Dtos;
using AutoMapper;
using SplitBackDotnet.Helper;
using Microsoft.AspNetCore.Authorization;
using SplitBackDotnet.Extensions;

namespace SplitBackDotnet.Endpoints;

public static class GroupEndpoints
{
  public static void MapGroupEndpoints(this IEndpointRouteBuilder app)
  {
    app.MapPost("/creategroup", [Authorize] async (HttpContext httpContext, IRepo repo, IMapper mapper, DataContext context, CreateGroupDto createGroupDto) =>
{
  try
  {
    var authedUserId = httpContext.GetAuthorizedUserId();
    var group = mapper.Map<Group>(createGroupDto);

    var newCreator = new User { UserId = authedUserId };
    context.Attach<User>(newCreator);

    group.AddAsCreatorAndMember(newCreator);
    await repo.CreateGroup(group);
    await repo.SaveChangesAsync();

    return Results.Ok();
  }
  catch (Exception ex)
  {
    return Results.BadRequest(ex.Message);
  }
});
  }

}