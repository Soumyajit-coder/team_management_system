namespace team_management_system.DTO
{
    public class OrganizationDTO
    {
        public int Id { get; set; }
        public string? OrgName { get; set;}
        public string? Slug { get; set;}
        public string? Description { get; set;}
        public int? OwnerUserId { get; set;}
    }
}
