using System.ComponentModel.DataAnnotations;
namespace SplitBackDotnet.Dtos;

public interface IExpenseDto
{
  public string GroupId { get; set; }
  [MaxLength(80)]
  public string Description { get; set; }
  [MaxLength(29)]
  public string Amount { get; set; }
  public bool SplitEqually { get; set; }
  [MaxLength(3)]
  public string IsoCode { get; set; }
  public LabelDto? Label { get; set; }
  public ICollection<ExpenseParticipantDto> ExpenseParticipants { get; set; }
  public ICollection<ExpenseSpenderDto> ExpenseSpenders { get; set; }

}