namespace SplitBackDotnet.Models;

public class AlphaSplitDatabaseSettings
{
  public string ConnectionString { get; set; } = null!;
  public string DatabaseName { get; set; } = null!;
  public string GroupCollection { get; set; } = null!;
  public string ExpenseCollection { get; set; } = null!;
  public string UserCollection { get; set; } = null!;
  public string InvitationCollection { get; set; } = null!;
  public string SessionCollection { get; set; } = null!;
  public string ActionCollection { get; set; } = null!;

}
