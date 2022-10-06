namespace SplitBackDotnet.Models; 

public class Session {
  public int Id { get; set; }
  public string RefreshToken { get; set; } = null!;
  public User User { get; set; } = null!;
  public string? Unique { get; set; }  
}
