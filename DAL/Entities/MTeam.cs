using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace team_management_system.DAL.Entities;

[Table("m_team")]
[Index("OrgId", "TeamName", Name = "uq_m_team_org_name", IsUnique = true)]
public partial class MTeam
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("org_id")]
    public long OrgId { get; set; }

    [Column("team_name")]
    [StringLength(100)]
    public string TeamName { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [Column("team_lead_id")]
    public int? TeamLeadId { get; set; }

    [Column("is_active")]
    public short IsActive { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime UpdatedAt { get; set; }

    [ForeignKey("OrgId")]
    [InverseProperty("MTeams")]
    public virtual MOrganization Org { get; set; } = null!;

    [InverseProperty("Team")]
    public virtual ICollection<TaskMgmt> TaskMgmts { get; set; } = new List<TaskMgmt>();

    [ForeignKey("TeamLeadId")]
    [InverseProperty("MTeams")]
    public virtual User? TeamLead { get; set; }

    [InverseProperty("Team")]
    public virtual ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();
}
