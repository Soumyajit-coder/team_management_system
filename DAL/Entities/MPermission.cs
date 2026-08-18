using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace team_management_system.DAL.Entities;

[Table("m_permission")]
public partial class MPermission
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("permission_name")]
    [StringLength(50)]
    public string? PermissionName { get; set; }

    [Column("slug")]
    [StringLength(20)]
    public string? Slug { get; set; }

    [InverseProperty("Permission")]
    public virtual ICollection<MRoleHasPermission> MRoleHasPermissions { get; set; } = new List<MRoleHasPermission>();
}
