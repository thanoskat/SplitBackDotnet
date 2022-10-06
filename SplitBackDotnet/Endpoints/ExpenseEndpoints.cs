using Microsoft.EntityFrameworkCore;
using SplitBackDotnet.Data;
using SplitBackDotnet.Models;

namespace SplitBackDotnet.Endpoints;

public static class ExpenseEndpoints {
  public static void MapExpenseEndpoints(this IEndpointRouteBuilder app) {
    
    app.MapGet("/expense", async (DataContext context) => await context.Expenses.ToListAsync());

    app.MapPost("/expense", async (DataContext context, Expense expense) => {
      User? userFound = context.Users.FirstOrDefault(user => user.Nickname == "nanakis");
      context.Add(expense);
      await context.SaveChangesAsync();
      return await context.Expenses.ToArrayAsync();
    });
  }
}
