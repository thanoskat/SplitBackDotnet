using Microsoft.EntityFrameworkCore;
using SplitBackDotnet.Data;
using SplitBackDotnet.Models;
using SplitBackDotnet.Dtos;
using AutoMapper;
using SplitBackDotnet.Helper;
using Microsoft.AspNetCore.Authorization;

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

    app.MapPost("/creategroup", [Authorize] async (HttpContext httpContext,IRepo repo, IMapper mapper, DataContext context, CreateGroupDto createGroupDto) => {
      
      var group = mapper.Map<Group>(createGroupDto);
      PreGroupSetUp.AddCreatorToMembers(context,httpContext,group);
      await repo.CreateGroup(group);
      await repo.SaveChangesAsync();

    });
  }
}