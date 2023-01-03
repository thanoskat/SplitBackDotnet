using SplitBackDotnet.Models;
namespace SplitBackDotnet.Dtos;

public class InvitationDto
{
  public string GroupId { get; set; } = null!;
  public string InviterId { get; set; } = null!;
}