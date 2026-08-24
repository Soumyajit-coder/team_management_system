namespace team_management_system.DTO
{
    public class OrganizationMembersDTO
    {
        public int OrgId { get; set; }
        public int UserId { get; set; }
        public string Role { get; set; } = null!;
        public DateTime JoinedAt { get; set; }
    }
}
