using System.ComponentModel.DataAnnotations;
namespace SplitBackDotnet.Dtos
{
  public class LabelDto
  {
    [MaxLength(30)]
    public string Name { get; set; } = null!;
  }

  public class ExpenseSpenderDto
  {
    [MaxLength(20)]
    public string SpenderId { get; set; }= null!;
    [MaxLength(29)]
    public string SpenderAmount { get; set; } = null!;
  }

  public class ExpenseParticipantDto
  {
    [MaxLength(20)]
    public string ParticipantId { get; set; }= null!;
    [MaxLength(29)]
    public string ContributionAmount { get; set; } = null!;
  }

  public class NewExpenseDto
  {
    public int GroupId { get; set; }
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
}