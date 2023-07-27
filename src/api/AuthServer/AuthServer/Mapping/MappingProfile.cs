using AuthServer.DTO;
using AutoMapper;

namespace AuthServer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterDTO, ApplicationUser>();

            CreateMap<ApplicationUser, UserDTO>();
        }
    }
}
