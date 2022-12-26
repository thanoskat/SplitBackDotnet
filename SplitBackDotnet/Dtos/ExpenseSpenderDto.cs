using System.ComponentModel.DataAnnotations;
namespace SplitBackDotnet.Dtos;
  public class ExpenseSpenderDto
  {
    [MaxLength(20)]
    public string SpenderId { get; set; }= null!;
    [MaxLength(29)]
    public string SpenderAmount { get; set; } = null!;
  }