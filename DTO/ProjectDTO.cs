using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace team_management_system.DTO
{
    public class ProjectDTO
    {
        //public int Id { get; set; }
        public int OrgId { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public DateOnly Deadline { get; set; }
    }

    public class ProjectDetailsDTO
    {
        public string OrgName { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public DateOnly? FromDate { get; set; }
        public DateOnly? ToDate { get; set; }
        public DateOnly? Deadline { get; set; }
    }
}
