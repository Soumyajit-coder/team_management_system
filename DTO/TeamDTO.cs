using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace team_management_system.DTO
{
    public class TeamDTO
    {
        public long OrgId { get; set; }
        public string TeamName { get; set; } = null!;
        public string? Description { get; set; }
        public int TeamLeadId { get; set; }
    }
}
