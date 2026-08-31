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
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<MPermission, PermissionDTO>().ReverseMap();
            CreateMap<MOrganization, OrganizationDTO>().ReverseMap();
            CreateMap<OrganizationMember, OrganizationMembersDTO>().ReverseMap();
            CreateMap<MTeam, TeamDTO>().ReverseMap();
            CreateMap<TeamMember, TeamMemberDTO>().ReverseMap();
            CreateMap<MProject, ProjectDTO>().ReverseMap();
            CreateMap<ProjectMember, ProjectMemberDTO>().ReverseMap();
        }
    }
}
