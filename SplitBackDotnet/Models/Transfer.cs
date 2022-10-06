namespace SplitBackDotnet.Models;

public class Transfer
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public User Sender { get; set; } = null!;
    public User Receiver { get; set; } = null!;
}
