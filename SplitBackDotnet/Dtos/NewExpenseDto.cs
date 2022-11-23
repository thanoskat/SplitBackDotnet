
namespace SplitBackDotnet.Dtos
{

  public class LabelDto
  {
    public string Name { get; set; } = null!;
  }

  public class ExpenseSpenderDto
  {
    public int SpenderId { get; set; }
    public decimal SpenderAmount { get; set; }
  }

  // public class UserShareDto
  // {
  //   public int Id { get; set; }
  // }

  public class ExpenseParticipantDto
  {
    public int ParticipantId { get; set; }
    public decimal ContributionAmount { get; set; }

  }

  public class NewExpenseDto
  {
    public int GroupId { get; set; }
    public string Description { get; set; } = null!;
    public decimal Amount { get; set; }
    public bool SplitEqually { get; set; }
    public LabelDto? Label { get; set; }
    public ICollection<ExpenseParticipantDto> ExpenseParticipants { get; set; } = null!;
    public ICollection<ExpenseSpenderDto> ExpenseSpenders { get; set; } = null!;
  }
}