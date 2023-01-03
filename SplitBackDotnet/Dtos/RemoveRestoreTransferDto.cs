using System.ComponentModel.DataAnnotations;
namespace SplitBackDotnet.Dtos;

public class RemoveRestoreTransferDto
{
  [MaxLength(20)]
  public string GroupId { get; set; } = null!;
  [MaxLength(20)]
  public string TransferId { get; set; } = null!;
  public bool Remove { get; set; }
}