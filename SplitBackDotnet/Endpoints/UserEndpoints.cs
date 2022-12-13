using SplitBackDotnet.Data;
using SplitBackDotnet.Models;
using Microsoft.EntityFrameworkCore;
using SplitBackDotnet.Dtos;
using AutoMapper;

namespace SplitBackDotnet.Endpoints;

public static class UserEndpoints {
  public static void MapUserEndpoints(this IEndpointRouteBuilder app) {

    //app.MapGet("/user", GetUsers);

    //app.MapGet("/user/{id}", async (DataContext context, int id) =>
    //  await context.Users.FindAsync(id) is User user ?
    //    Results.Ok(user) :
    //    Results.NotFound("Sorry, user not found. <3")
    //);

    //app.MapPost("/user", AddUser);

    //app.MapPut("/user/{id}", async (DataContext context, User user, int id) => {
    //  var dbUser = await context.Users.FindAsync(id);
    //  if (dbUser == null) return Results.NotFound("No user found <3");

    //  dbUser.Nickname = user.Nickname;
    //  dbUser.Email = user.Email;
    //  await context.SaveChangesAsync();

    //  return Results.Ok(await context.Users.ToArrayAsync());
    //});

    //app.MapDelete("/user/{id}", async (DataContext context, int id) => {
    //  var dbUser = await context.Users.FindAsync(id);
    //  if (dbUser == null) return Results.NotFound("Who dis? <3");

    //  context.Users.Remove(dbUser);
    //  await context.SaveChangesAsync();
    //  return Results.Ok(await context.Users.ToArrayAsync());
    //});
  }

  //public static async Task<Results<Ok<List<User>>, BadRequest<string>>> GetUsers(DataContext context) {}


  // public static async Task<IResult> GetUsers(DataContext context) {
  //   // return TypedResults.BadRequest("lala");
  //   return TypedResults.Ok(await context.Users.Include(user => user.CreatedGroups).ToListAsync());
  // }

  // public static async Task<IResult> AddUser(DataContext context, IMapper mapper, UserCreateDto userCreateDto)
  // {
  //   var newUser = mapper.Map<User>(userCreateDto);
  //   context.Users.Add(newUser);
  //   await context.SaveChangesAsync();
  //   return TypedResults.Ok(await context.Users.ToArrayAsync());
  // }

}
