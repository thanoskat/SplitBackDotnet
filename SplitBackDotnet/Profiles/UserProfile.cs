using AutoMapper;
using SplitBackDotnet.Dtos;
using SplitBackDotnet.Models;

namespace SplitBackDotnet.Profiles;

public class UserProfile : Profile {
  public UserProfile() {
    //source ->target
    CreateMap<UserCreateDto, User>();
    CreateMap<CreateGroupDto, Group>()
    .ForMember(dest => dest.Labels, opt => opt.MapFrom(src => src.GroupLabels));
  }
}
  