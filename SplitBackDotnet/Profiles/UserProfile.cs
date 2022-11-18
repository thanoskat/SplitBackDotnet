using AutoMapper;
using SplitBackDotnet.Dtos;
using SplitBackDotnet.Models;

namespace SplitBackDotnet.Profiles;

public class UserProfile : Profile
{
  public UserProfile()
  {
    //source ->target
    CreateMap<UserCreateDto, User>();
    CreateMap<LabelDto, Label>();
    //CreateMap<UserShareDto, User>();
    CreateMap<ShareDto, Share>();
    CreateMap<ExpenseUsersDto, ExpenseUser>();
    CreateMap<NewExpenseDto, Expense>();
    CreateMap<CreateGroupDto, Group>()
    .ForMember(dest => dest.Labels, opt => opt.MapFrom(src => src.GroupLabels));
  }
}