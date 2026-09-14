using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace team_management_system.DTO
{
    public class TaskMgmtDTO
    {
        public long Id { get; set; }
        public long OrganizationId { get; set; }
        public long? ProjectId { get; set; }
        public long TeamId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public short Priority { get; set; }
        public short Status { get; set; }
        public long CreatedBy { get; set; }
        public long? AssignedBy { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? DueDate { get; set; }
        public DateTime? CompletedAt { get; set; }
    }

    public class TaskMgmtDetailsDTO 
    {
        public string OrganizationName { get; set; }
        public string ProjectName { get; set; }
        public string TeamName { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
    }
}
