using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace team_management_system.DAL.Entities;

[Table("task_assignees")]
[Index("TaskId", "AssignedTo", Name = "task_assignees_task_id_assigned_to_key", IsUnique = true)]
public partial class TaskAssignee
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("task_id")]
    public long TaskId { get; set; }

    [Column("assigned_to")]
    public int AssignedTo { get; set; }

    [Column("assigned_by")]
    public int AssignedBy { get; set; }

    [Column("role")]
    public short? Role { get; set; }

    [Column("assigned_at", TypeName = "timestamp without time zone")]
    public DateTime? AssignedAt { get; set; }

    [Column("is_active")]
    public short? IsActive { get; set; }

    [ForeignKey("AssignedBy")]
    [InverseProperty("TaskAssigneeAssignedByNavigations")]
    public virtual User AssignedByNavigation { get; set; } = null!;

    [ForeignKey("AssignedTo")]
    [InverseProperty("TaskAssigneeAssignedToNavigations")]
    public virtual User AssignedToNavigation { get; set; } = null!;

    [ForeignKey("TaskId")]
    [InverseProperty("TaskAssignees")]
    public virtual TaskMgmt Task { get; set; } = null!;
}
