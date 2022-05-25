#pragma warning disable CS8618

using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using TaskManager.Core.Models.Shared;

namespace TaskManager.Core.Models.Entities;

public class ErrandEntity
{
    public uint Id { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }
    public ReportFormatEntity ReportFormat { get; set; }
    public string Type { get; set; }
    public UserEntity? Leader { get; set; }
    public List<UserEntity> Users { get; set; }
    public DateTime Started { get; set; }
    public DateTime? Deadline { get; set; }
    public TaskState State { get; set; }
    public ReportEntity? Report { get; set; }
    public string? ReviewComment { get; set; }
}
