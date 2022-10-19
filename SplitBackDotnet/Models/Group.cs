using System.Text.Json.Serialization;

namespace SplitBackDotnet.Models;

public class Group {
  public int Id { get; set; }
  public string Title { get; set; } = null!;
  public User Creator { get; set; } = null!;
  public ICollection<User> Members { get; set; } = null!;
  public ICollection<Expense>? Expenses { get; set; }
  public ICollection<Transfer>? Transfers { get; set; }
  public ICollection<Label>? Labels { get; set; }
}
