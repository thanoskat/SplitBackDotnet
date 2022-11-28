namespace SplitBackDotnet.Models;

public class EmailSettings {
  public string Host { get; set; } = null!;
  public string Username { get; set; } = null!;
  public string Password { get; set; } = null!;
  public int Port { get; set; }
}
