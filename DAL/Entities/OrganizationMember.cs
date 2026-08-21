using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace team_management_system.DAL.Entities;

[Table("organization_members")]
[Index("OrgId", "UserId", Name = "uq_organization_members", IsUnique = true)]
public partial class OrganizationMember
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("org_id")]
    public long OrgId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("role")]
    [StringLength(30)]
    public string Role { get; set; } = null!;

    [Column("is_active")]
    public short IsActive { get; set; }

    [Column("joined_at", TypeName = "timestamp without time zone")]
    public DateTime JoinedAt { get; set; }

    [ForeignKey("OrgId")]
    [InverseProperty("OrganizationMembers")]
    public virtual MOrganization Org { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("OrganizationMembers")]
    public virtual User User { get; set; } = null!;
}
