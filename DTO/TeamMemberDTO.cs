using System.ComponentModel.DataAnnotations.Schema;

namespace team_management_system.DTO
{
    public class TeamMemberDTO
    {
        public int TeamId { get; set; }
        public int UserId { get; set; }
        public DateOnly Doj { get; set; }
    }

    public class TeamMemberDetailsDTO
    {
        public string TeamName { get; set; }
        public string UserName { get; set; }
        public DateOnly? Doj { get; set; }
        public string IsActive { get; set; }
    }
}
