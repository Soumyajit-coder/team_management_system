using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace team_management_system.DTO
{
    public class ProjectMemberDTO
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public string Role { get; set; } = null!;
        public DateTime JoinedAt { get; set; }
    }

    public class ProjectMemberDetailsDTO
    {
        public string ProjectName { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; } = null!;
        public DateTime JoinedAt { get; set; }
    }
}
