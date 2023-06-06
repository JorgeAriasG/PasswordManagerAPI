using AutoMapper;
using password_manager.api.Dtos.User;
using password_manager.api.Models;

namespace password_manager.api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<LoginUserDto, User>();
            CreateMap<User, ReadUserDto>();
        }
    }
}