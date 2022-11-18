namespace SplitBackDotnet.Models; 
using System.ComponentModel.DataAnnotations;

public class Session {
  public int SessionId { get; set; }
  [Required]
  public string RefreshToken { get; set; } =String.Empty;
  [Required]
  public User User { get; set; } = new User();
  public string? Unique { get; set; }  
}
