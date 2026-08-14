using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace team_management_system.DAL.Entities;

[Table("designations")]
public partial class Designation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("designation_name")]
    [StringLength(50)]
    public string? DesignationName { get; set; }
}
