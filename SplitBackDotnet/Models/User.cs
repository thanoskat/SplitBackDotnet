using System.ComponentModel.DataAnnotations;
namespace SplitBackDotnet.Models;

public class User
{
  public int UserId { get; set; }
  
  [MaxLength(200)]
  [Required]
  public string Nickname { get; set; } = String.Empty;
  
  [MaxLength(200)]
  [Required]
  public string Email { get; set; } = String.Empty;
  
  public ICollection<Group> Groups { get; set; } = new List<Group>();
  
  public ICollection<Group> CreatedGroups { get; set; } = new List<Group>();
  
  [Required]
  public ICollection<ExpenseSpender> ExpenseSpenders { get; set; } = new List<ExpenseSpender>();
  
  public ICollection<ExpenseParticipant> ExpenseParticipants { get; set; } = new List<ExpenseParticipant>();
}
