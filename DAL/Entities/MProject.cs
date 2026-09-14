using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace team_management_system.DAL.Entities;

[Table("m_project")]
[Index("OrgId", "ProjectName", Name = "uq_m_project_org_name", IsUnique = true)]
[Index("OrgId", "Slug", Name = "uq_m_project_org_slug", IsUnique = true)]
public partial class MProject
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("org_id")]
    public long OrgId { get; set; }

    [Column("project_name")]
    [StringLength(150)]
    public string ProjectName { get; set; } = null!;

    [Column("slug")]
    [StringLength(150)]
    public string Slug { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [Column("from_date")]
    public DateOnly FromDate { get; set; }

    [Column("to_date")]
    public DateOnly ToDate { get; set; }

    [Column("deadline")]
    public DateOnly Deadline { get; set; }

    [Column("status")]
    public short Status { get; set; }

    [Column("created_by")]
    public int CreatedBy { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime UpdatedAt { get; set; }

    [ForeignKey("OrgId")]
    [InverseProperty("MProjects")]
    public virtual MOrganization Org { get; set; } = null!;

    [InverseProperty("Project")]
    public virtual ICollection<TaskMgmt> TaskMgmts { get; set; } = new List<TaskMgmt>();
}
