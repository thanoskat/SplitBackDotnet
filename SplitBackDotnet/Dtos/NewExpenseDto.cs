
using System.ComponentModel.DataAnnotations;
using NMoneys;
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

  // public class UserShareDto
  // {
  //   public int Id { get; set; }
  // }

  public class ExpenseParticipantDto
  {
    public int ParticipantId { get; set; }
    public decimal ContributionAmount { get; set; }

  }
  public class CurrencyDto
  {
    public CurrencyIsoCode CurrencyIsoCode { get; set; }
  }

  public class NewExpenseDto
  {
    public int GroupId { get; set; }
    [MaxLength(80)]
    public string Description { get; set; } = null!;
    public decimal Amount { get; set; }
    public bool SplitEqually { get; set; }
    [MaxLength(3)]
    public CurrencyDto Currency { get; set; } = null!;
    public LabelDto? Label { get; set; }
    public ICollection<ExpenseParticipantDto> ExpenseParticipants { get; set; } = null!;
    public ICollection<ExpenseSpenderDto> ExpenseSpenders { get; set; } = null!;
  }
}