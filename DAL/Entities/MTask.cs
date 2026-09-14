using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace team_management_system.DAL.Entities;

[Table("m_task")]
public partial class MTask
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("organization_id")]
    public long OrganizationId { get; set; }

    [Column("project_id")]
    public long? ProjectId { get; set; }

    [Column("team_id")]
    public long TeamId { get; set; }

    [Column("title")]
    [StringLength(200)]
    public string Title { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [Column("priority")]
    public short Priority { get; set; }

    [Column("status")]
    public short Status { get; set; }

    [Column("created_by")]
    public long CreatedBy { get; set; }

    [Column("assigned_by")]
    public long? AssignedBy { get; set; }

    [Column("start_date")]
    public DateOnly? StartDate { get; set; }

    [Column("due_date")]
    public DateOnly? DueDate { get; set; }

    [Column("completed_at", TypeName = "timestamp without time zone")]
    public DateTime? CompletedAt { get; set; }

    [Column("estimated_hours")]
    [Precision(8, 2)]
    public decimal? EstimatedHours { get; set; }

    [Column("actual_hours")]
    [Precision(8, 2)]
    public decimal? ActualHours { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "timestamp without time zone")]
    public DateTime? DeletedAt { get; set; }

    [ForeignKey("OrganizationId")]
    [InverseProperty("MTasks")]
    public virtual MOrganization Organization { get; set; } = null!;

    [ForeignKey("ProjectId")]
    [InverseProperty("MTasks")]
    public virtual MProject? Project { get; set; }

    [InverseProperty("Task")]
    public virtual ICollection<TaskAssignee> TaskAssignees { get; set; } = new List<TaskAssignee>();

    [InverseProperty("Task")]
    public virtual ICollection<TaskComment> TaskComments { get; set; } = new List<TaskComment>();

    [ForeignKey("TeamId")]
    [InverseProperty("MTasks")]
    public virtual MTeam Team { get; set; } = null!;
}
