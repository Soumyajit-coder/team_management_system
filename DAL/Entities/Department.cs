using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace team_management_system.DAL.Entities;

[Table("departments")]
public partial class Department
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("department_name")]
    [StringLength(50)]
    public string? DepartmentName { get; set; }
}
