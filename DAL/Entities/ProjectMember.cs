using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace team_management_system.DAL.Entities;

[Table("project_members")]
[Index("ProjectId", "UserId", Name = "uq_project_members", IsUnique = true)]
public partial class ProjectMember
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("project_id")]
    public long ProjectId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("role")]
    [StringLength(30)]
    public string Role { get; set; } = null!;

    [Column("joined_at", TypeName = "timestamp without time zone")]
    public DateTime JoinedAt { get; set; }

    [Column("is_active")]
    public short IsActive { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("ProjectMembers")]
    public virtual User User { get; set; } = null!;
}
