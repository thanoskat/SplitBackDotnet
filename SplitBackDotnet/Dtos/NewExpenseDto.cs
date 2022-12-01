
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
    public int SpenderId { get; set; }
    public decimal SpenderAmount { get; set; }
  }

  public class ExpenseParticipantDto
  {
    public int ParticipantId { get; set; }
    public decimal ContributionAmount { get; set; }

  }

  public class NewExpenseDto
  {
    public int GroupId { get; set; }
    [MaxLength(80)]
    public string Description { get; set; } = null!;
    public string Amount { get; set; } = null!;
    public bool SplitEqually { get; set; }
    public string IsoCode { get; set; } = null!;
    public LabelDto? Label { get; set; }
    public ICollection<ExpenseParticipantDto> ExpenseParticipants { get; set; } = null!;
    public ICollection<ExpenseSpenderDto> ExpenseSpenders { get; set; } = null!;
  }
}