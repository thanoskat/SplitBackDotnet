namespace SplitBackDotnet.Models;
using System.ComponentModel.DataAnnotations;

public class Label
{
  public int LabelId { get; set; }
  [MaxLength(50)]
  public string Name { get; set; } = null!;
}
