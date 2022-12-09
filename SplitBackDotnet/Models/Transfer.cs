namespace SplitBackDotnet.Models;
using System.ComponentModel.DataAnnotations;
public class Transfer
{
  public int TransferId { get; set; }
  [MaxLength(80)]
  public string? Description { get; set; }
  public decimal Amount { get; set; }
  public string IsoCode { get; set; } = null!;
  public int SenderId { get; set; }
  public int ReceiverId { get; set; }
  public Group Group { get; set; } = null!;
  public int GroupId { get; set; }
  public DateTime CreatedAt {get;set;}
}
