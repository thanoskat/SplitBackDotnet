using AutoMapper;
using SplitBackDotnet.Dtos;
using SplitBackDotnet.Models;
using MongoDB.Bson;

namespace SplitBackDotnet.Validators;

public class StringToObjectIdConverter : ITypeConverter<string, ObjectId>
{
  public ObjectId Convert(string source, ObjectId destination, ResolutionContext context)
  {
    return ObjectId.Parse(source);
  }
}
public class UserProfile : Profile
{
  public UserProfile()
  {
    //source ->target
    CreateMap<UserCreateDto, User>();

    CreateMap<LabelDto, Label>();
    CreateMap<ExpenseParticipantDto, ExpenseParticipant>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ParticipantId));
    CreateMap<ExpenseSpenderDto, ExpenseSpender>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SpenderId));
    CreateMap<NewExpenseDto, Expense>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => ObjectId.GenerateNewId()));
    CreateMap<EditExpenseDto, Expense>();
    CreateMap<NewTransferDto, Transfer>();
    //.ForMember(dest => dest.Currency, opt => opt.Ignore());
    CreateMap<CreateGroupDto, Group>();
    CreateMap<Expense, ExpenseSnapShot>()
    //.ForMember(dest => dest.Id, opt => opt.Ignore())
    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => ObjectId.GenerateNewId()));
    CreateMap<NewCommentDto, Comment>().ForMember(dest=>dest.Id, opt => opt.MapFrom(src => ObjectId.GenerateNewId()));
  }
}