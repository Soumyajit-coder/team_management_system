using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace team_management_system.DAL.Entities;

[Table("roles")]
public partial class Role
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("role_name")]
    [StringLength(30)]
    public string? RoleName { get; set; }

    [Column("slug")]
    [StringLength(30)]
    public string? Slug { get; set; }

    [InverseProperty("Role")]
    public virtual ICollection<UserHasRole> UserHasRoles { get; set; } = new List<UserHasRole>();
}
