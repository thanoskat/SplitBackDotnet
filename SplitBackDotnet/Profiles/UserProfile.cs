using AutoMapper;
using SplitBackDotnet.Dtos;
using SplitBackDotnet.Models;

namespace SplitBackDotnet.Profiles; 

public class UserProfile : Profile {
  public UserProfile() {
    CreateMap<UserCreateDto, User>();
  }
}
