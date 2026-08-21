using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace team_management_system.DAL.Entities;

[Table("m_organization")]
[Index("OrgName", Name = "uq_m_organization_name", IsUnique = true)]
[Index("Slug", Name = "uq_m_organization_slug", IsUnique = true)]
public partial class MOrganization
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("org_name")]
    [StringLength(100)]
    public string OrgName { get; set; } = null!;

    [Column("slug")]
    [StringLength(100)]
    public string Slug { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [Column("owner_user_id")]
    public int OwnerUserId { get; set; }

    [Column("is_active")]
    public short IsActive { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime UpdatedAt { get; set; }

    [InverseProperty("Org")]
    public virtual ICollection<MProject> MProjects { get; set; } = new List<MProject>();

    [InverseProperty("Org")]
    public virtual ICollection<MTeam> MTeams { get; set; } = new List<MTeam>();

    [InverseProperty("Org")]
    public virtual ICollection<OrganizationMember> OrganizationMembers { get; set; } = new List<OrganizationMember>();

    [ForeignKey("OwnerUserId")]
    [InverseProperty("MOrganizations")]
    public virtual User OwnerUser { get; set; } = null!;
}
