using Microsoft.EntityFrameworkCore;
using SplitBackDotnet.Data;
using SplitBackDotnet.Models;
using SplitBackDotnet.Dtos;
using AutoMapper;
using SplitBackDotnet.Helper;
using Microsoft.AspNetCore.Mvc;
namespace SplitBackDotnet.Endpoints;

public static class ExpenseEndpoints {
  public static void MapExpenseEndpoints(this IEndpointRouteBuilder app) {

    app.MapGet("/expense", async (DataContext context) => await context.Expenses.ToListAsync());

    app.MapPost("/expense", async (DataContext context, Expense expense) => {
      User? userFound = context.Users.FirstOrDefault(user => user.Nickname == "nanakis");
      context.Add(expense);
      await context.SaveChangesAsync();
      return await context.Expenses.ToArrayAsync();
    }).RequireAuthorization();

    app.MapGet("/test", () => {
      string x = "This is a test route";
      Console.WriteLine(x);
    });

    app.MapPost("/creategroup", async (IRepo repo, IMapper mapper, DataContext context, CreateGroupDto createGroupDto) => {
      var group = mapper.Map<Group>(createGroupDto);
      User creator = new User();
      creator.Id=7;
      creator.Email="paok@gmail.com";
      creator.Nickname="Mpelos";
      Console.WriteLine(creator.Id);
      AddCreatorAsMember.AddCreatorToMembers(creator,group);
      Console.Write("Hello");

      //bool userFound = new UserLookup(context).UserFound(userr, group);
      await repo.CreateGroup(group);
      await repo.SaveChangesAsync();
      // await context.Groups.AddAsync(group);
      // await context.SaveChangesAsync();
    });
  }
}
