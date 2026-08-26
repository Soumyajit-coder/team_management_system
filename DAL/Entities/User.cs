using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace team_management_system.DAL.Entities;

[Table("users", Schema = "employee")]
[Index("Email", Name = "user_email_unique", IsUnique = true)]
[Index("MobileNo", Name = "users_mobile_unique", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("user_name")]
    [StringLength(200)]
    public string UserName { get; set; } = null!;

    [Column("email")]
    [StringLength(250)]
    public string Email { get; set; } = null!;

    [Column("mobile_no")]
    [StringLength(10)]
    public string MobileNo { get; set; } = null!;

    [Column("password_hash")]
    public byte[] PasswordHash { get; set; } = null!;

    [Column("password_salt")]
    public byte[] PasswordSalt { get; set; } = null!;

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("OwnerUser")]
    public virtual ICollection<MOrganization> MOrganizations { get; set; } = new List<MOrganization>();

    [InverseProperty("TeamLead")]
    public virtual ICollection<MTeam> MTeams { get; set; } = new List<MTeam>();

    [InverseProperty("User")]
    public virtual ICollection<OrganizationMember> OrganizationMembers { get; set; } = new List<OrganizationMember>();

    [InverseProperty("User")]
    public virtual ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();

    [InverseProperty("User")]
    public virtual ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();

    [InverseProperty("User")]
    public virtual ICollection<UserHasRole> UserHasRoles { get; set; } = new List<UserHasRole>();
}
