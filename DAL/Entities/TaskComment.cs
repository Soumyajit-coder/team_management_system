using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace team_management_system.DAL.Entities;

[Table("task_comments")]
public partial class TaskComment
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("task_id")]
    public long TaskId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("comment")]
    public string Comment { get; set; } = null!;

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("TaskId")]
    [InverseProperty("TaskComments")]
    public virtual TaskMgmt Task { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("TaskComments")]
    public virtual User User { get; set; } = null!;
}
