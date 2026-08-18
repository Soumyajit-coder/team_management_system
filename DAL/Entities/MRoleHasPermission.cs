using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace team_management_system.DAL.Entities;

[Table("m_role_has_permission")]
[Index("RoleId", "PermissionId", Name = "uq_m_role_has_permission", IsUnique = true)]
public partial class MRoleHasPermission
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("role_id")]
    public int RoleId { get; set; }

    [Column("permission_id")]
    public int PermissionId { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("PermissionId")]
    [InverseProperty("MRoleHasPermissions")]
    public virtual MPermission Permission { get; set; } = null!;

    [ForeignKey("RoleId")]
    [InverseProperty("MRoleHasPermissions")]
    public virtual Role Role { get; set; } = null!;
}
