using AutoMapper;
using password_manager.api.Dtos.Password;
using password_manager.api.Models;

namespace password_manager.api.Profiles
{
    public class PasswordProfile : Profile
    {
        public PasswordProfile()
        {
            CreateMap<CreatePasswordDto, Password>();
            CreateMap<Password, ReadPasswordDto>();
            CreateMap<UpdatePasswordDto, Password>();
        }
    }
}