using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace team_management_system.DAL.Entities;

[Keyless]
[Table("m_role_has_permission")]
[Index("RoleId", "PermissionId", Name = "uq_m_role_has_permission", IsUnique = true)]
public partial class MRoleHasPermission
{
    [Column("id")]
    public int Id { get; set; }

    [Column("role_id")]
    public int RoleId { get; set; }

    [Column("permission_id")]
    public int PermissionId { get; set; }

    [ForeignKey("PermissionId")]
    public virtual MPermission Permission { get; set; } = null!;

    [ForeignKey("RoleId")]
    public virtual Role Role { get; set; } = null!;
}
