namespace SplitBackDotnet.Models;

public class Share
{
    public int Id { get; set; }
    public User User { get; set; } = null!;
    public decimal Amount { get; set; }
}
