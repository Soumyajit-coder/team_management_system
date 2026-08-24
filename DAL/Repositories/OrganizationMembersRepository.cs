using Microsoft.EntityFrameworkCore;
using team_management_system.DAL.Entities;
using team_management_system.DAL.Interfaces;
using team_management_system.DTO;

namespace team_management_system.DAL.Repositories
{
    public class OrganizationMembersRepository : Repository<OrganizationMember>, IOrganizationMembersRepository
    {
        private readonly teamManagementSystemDBContext _dbContext;
        private DbSet<OrganizationMember> _dbSet;
        public OrganizationMembersRepository(teamManagementSystemDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<OrganizationMember>();
        }
        public async Task<String> GetNameByIdAsync(int userId)
        {
            var userName = await (from om in _dbContext.OrganizationMembers join u in _dbContext.Users on om.UserId equals u.Id where om.UserId == userId select u.UserName).FirstOrDefaultAsync();
            return userName;
        }
        public async Task<List<String>> GetOrgNameByIdAsync(int orgId)
        {
            var orgName = await (from om in _dbContext.OrganizationMembers join org in _dbContext.MOrganizations on om.OrgId equals org.Id where om.OrgId == orgId select org.OrgName).ToListAsync();
            return orgName;
        }
        public async Task<List<OrganizationMembersDetailsDTO>> GetOrgMemeberListAsync()
        {
            var orgMembersList = await (from om in _dbContext.OrganizationMembers join org in _dbContext.MOrganizations on om.OrgId equals org.Id join u in _dbContext.Users on om.UserId equals u.Id
                                        select new OrganizationMembersDetailsDTO 
                                        {
                                            OrgName = org.OrgName,
                                            UserName = u.UserName,
                                            Role = om.Role
                                        }).ToListAsync();
            return orgMembersList;
        }
    }
}
