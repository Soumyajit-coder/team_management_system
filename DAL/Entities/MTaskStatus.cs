using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace team_management_system.DAL.Entities;

[Table("m_task_status")]
public partial class MTaskStatus
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("status_name")]
    public string? StatusName { get; set; }
}
