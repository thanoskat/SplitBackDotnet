using System.ComponentModel.DataAnnotations;
namespace SplitBackDotnet.Dtos;

  public class NewExpenseDto: IExpenseDto
  {
    public string GroupId { get; set; } = null!;
    [MaxLength(80)]
    public string Description { get; set; } = null!;
    [MaxLength(29)]
    public string Amount { get; set; } = null!;
    public bool SplitEqually { get; set; }
    [MaxLength(3)]
    public string IsoCode { get; set; } = null!;
    public LabelDto? Label { get; set; }
    public ICollection<ExpenseParticipantDto> ExpenseParticipants { get; set; } = null!;
    public ICollection<ExpenseSpenderDto> ExpenseSpenders { get; set; } = null!;
  }