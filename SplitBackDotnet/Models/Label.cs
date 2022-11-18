namespace SplitBackDotnet.Models;
using System.ComponentModel.DataAnnotations;

public class Label
{
    public int LabelId { get; set; }
    [MaxLength(100)]
    [Required]
    public string Name { get; set; } = String.Empty;
}
