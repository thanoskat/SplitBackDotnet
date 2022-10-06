namespace SplitBackDotnet.Models;

public class Expense
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public decimal Amount { get; set; }
    public User Spender { get; set; } = null!;
    public Label? Label { get; set; }
    public ICollection<Share> Shares { get; set; } = null!;
}
