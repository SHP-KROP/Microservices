using AuthServer.DTO;
using AutoMapper;

namespace AuthServer.Mapping
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<UserRegisterDTO, ApplicationUser>();

            CreateMap<ApplicationUser, UserDTO>();
        }
    }
}
