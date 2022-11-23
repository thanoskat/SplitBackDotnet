namespace SplitBackDotnet.Models;
using System.ComponentModel.DataAnnotations;
public class Transfer
{
    public int TransferId { get; set; }
    [MaxLength(100)]
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
}
