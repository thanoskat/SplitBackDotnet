using System.ComponentModel.DataAnnotations;
namespace SplitBackDotnet.Dtos;

  public class LabelDto
  {
    [MaxLength(30)]
    public string Name { get; set; } = null!;
  }