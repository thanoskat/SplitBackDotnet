using System.ComponentModel.DataAnnotations;
namespace SplitBackDotnet.Dtos;

public class TransactionHistoryDto
{
  [MaxLength(80)]
  public string GroupId { get; set; } = null!;
}