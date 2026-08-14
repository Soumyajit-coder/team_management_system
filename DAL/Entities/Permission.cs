using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace team_management_system.DAL.Entities;

[Table("permissions")]
public partial class Permission
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("permission_name")]
    [StringLength(100)]
    public string? PermissionName { get; set; }
}
