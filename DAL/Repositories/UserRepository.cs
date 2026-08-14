using team_management_system.DAL.Entities;
using team_management_system.DAL.Interfaces;

namespace team_management_system.DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(teamManagementSystemDBContext dbContext) : base(dbContext)
        {
        }
    }
}
