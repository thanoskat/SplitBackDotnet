using System.ComponentModel.DataAnnotations;
namespace SplitBackDotnet.Dtos;

public class RemoveExpenseDto
{
  [MaxLength(20)]
  public string GroupId { get; set; } = null!;
  [MaxLength(20)]
  public string ExpenseId { get; set; } = null!;
}