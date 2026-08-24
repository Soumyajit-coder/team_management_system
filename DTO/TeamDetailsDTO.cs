namespace team_management_system.DTO
{
    public class TeamDetailsDTO
    {
        public string OrgName { get; set; }
        public string TeamName { get; set; } = null!;
        public string Description { get; set; }
        public string TeamLeadName { get; set; }
    }
}
