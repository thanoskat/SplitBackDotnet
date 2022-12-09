namespace SplitBackDotnet.Models;
using System.ComponentModel.DataAnnotations;



public class HistoricalTransaction
{
  public int ExpenseId { get; set; }
  public DateTime CreatedAt { get; set; }
  [MaxLength(200)]
  public string? Description { get; set; }
  public decimal Lent { get; set; }
  public decimal Borrowed { get; set; }
  public decimal UserPaid { get; set; }
  public decimal UserShare { get; set; }
  public bool IsTransfer { get; set; }
  public decimal TotalLent { get; set; }
  public decimal TotalBorrowed { get; set; }
  public decimal Balance { get; set; }
}