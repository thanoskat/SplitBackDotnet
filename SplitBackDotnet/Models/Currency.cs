namespace SplitBackDotnet.Models;
using System.ComponentModel.DataAnnotations;

public class Currency
{
  // public int CurrencyId { get; set; }
  // [MaxLength(3)]
  //public string isoCode { get; set; } = null!;
  public short IsoCode {get;set;}
  public ICollection<Expense> Expenses { get; set; } = null!;
}