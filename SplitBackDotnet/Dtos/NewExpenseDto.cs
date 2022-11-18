
namespace SplitBackDotnet.Dtos
{

  public class LabelDto
  {
    public string Name { get; set; } = null!;
  }

  public class ExpenseUsersDto
  {
    public int SpenderId { get; set; }
    public decimal SpenderAmount { get; set; }
  }

  // public class UserShareDto
  // {
  //   public int Id { get; set; }
  // }

  public class ShareDto
  {
    public int ParticipantId { get; set; }
    public decimal ParticipantAmount { get; set; }

  }

  public class NewExpenseDto
  {
    public int GroupId { get; set; }
    public string Description { get; set; } = null!;
    public decimal Amount { get; set; }
    public LabelDto? Label { get; set; }
    public ICollection<ShareDto> Shares { get; set; } = null!;
    public ICollection<ExpenseUsersDto> ExpenseUsers { get; set; } = null!;
  }
}