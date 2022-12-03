using AutoMapper;
using SplitBackDotnet.Dtos;
using SplitBackDotnet.Models;

namespace SplitBackDotnet.Validators;

public class UserProfile : Profile
{
  public UserProfile()
  {
    //source ->target
    CreateMap<UserCreateDto, User>();
    CreateMap<LabelDto, Label>();
    CreateMap<ExpenseParticipantDto, ExpenseParticipant>();
    CreateMap<ExpenseSpenderDto, ExpenseSpender>();
    CreateMap<NewExpenseDto, Expense>();
    CreateMap<NewTransferDto, Transfer>();
    // .ForMember(dest => dest.Currency, opt => opt.Ignore());
    CreateMap<CreateGroupDto, Group>()
    .ForMember(dest => dest.Labels, opt => opt.MapFrom(src => src.GroupLabels));
  }
}