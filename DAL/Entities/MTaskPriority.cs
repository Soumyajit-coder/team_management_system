using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace team_management_system.DAL.Entities;

[Keyless]
[Table("m_task_priority")]
public partial class MTaskPriority
{
    [Column("id")]
    public long? Id { get; set; }

    [Column("priority_name")]
    [StringLength(10)]
    public string? PriorityName { get; set; }
}
