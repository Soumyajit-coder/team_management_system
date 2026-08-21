using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace team_management_system.DAL.Entities;

[Table("team_member")]
[Index("TeamId", "UserId", Name = "uq_team_member", IsUnique = true)]
public partial class TeamMember
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("team_id")]
    public long TeamId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("doj")]
    public DateOnly? Doj { get; set; }

    [Column("is_active")]
    public short IsActive { get; set; }

    [Column("joined_at", TypeName = "timestamp without time zone")]
    public DateTime JoinedAt { get; set; }

    [ForeignKey("TeamId")]
    [InverseProperty("TeamMembers")]
    public virtual MTeam Team { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("TeamMembers")]
    public virtual User User { get; set; } = null!;
}
