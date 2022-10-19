using System.Text.Json.Serialization;

namespace SplitBackDotnet.Models;

public class User {
  public int Id { get; set; }
  public string Nickname { get; set; } = null!;
  public string Email { get; set; } = null!;
  public ICollection<Group>? Groups { get; set; }
  public ICollection<Group>? CreatedGroups { get; set; }
  public ICollection<Expense>? Expenses { get; set; }
  public ICollection<ExpenseUser> ExpenseUsers { get; set; } = null!;
}
