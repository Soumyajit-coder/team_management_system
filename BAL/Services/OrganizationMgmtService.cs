using System.Net;
using AutoMapper;
using team_management_system.BAL.Interfaces;
using team_management_system.DAL.Entities;
using team_management_system.DAL.Interfaces;
using team_management_system.DTO;
using team_management_system.Helper;

namespace team_management_system.BAL.Services
{
    public class OrganizationMgmtService : IOrganizationMgmtService
    {
        private readonly IOrganizationMgmtRepository _organizationMgmtRepository;
        private IMapper _mapper;
        public OrganizationMgmtService(IOrganizationMgmtRepository organizationMgmtRepository, IMapper mapper)
        {
            _organizationMgmtRepository = organizationMgmtRepository;
            _mapper = mapper;
        }
        public async Task<long> CreateOrganizationAsync(OrganizationDTO dto)
        {
            MOrganization createOrganization = _mapper.Map<MOrganization>(dto);
            if (!string.IsNullOrEmpty(createOrganization.OrgName) && string.IsNullOrEmpty(createOrganization.Slug))
            {
                createOrganization.Slug = createOrganization.OrgName.Trim().ToLower().Replace(" ", "_");
            }
            await _organizationMgmtRepository.CreateAsync(createOrganization);
            return createOrganization.Id;            
        }
        public async Task<List<MOrganization>> GetOrganizationListAsync()
        {
            var orgList = await _organizationMgmtRepository.GetAllAsync();
            return orgList;
        }
        public async Task<MOrganization> GetOrganizationById(int id)
        {
            var orgDetails = await _organizationMgmtRepository.GetDetailsByIdAsync(id);
            return orgDetails;
        }
        public async Task<String> GetOrganizationByConditionAsync(string condition)
        {
            var orgName = await _organizationMgmtRepository.GetDetailsByNameAsync(condition);
            return orgName;
        }
    }
}
