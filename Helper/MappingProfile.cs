using AutoMapper;
using team_management_system.DAL.Entities;
using team_management_system.DTO;

namespace team_management_system.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDetailsDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
