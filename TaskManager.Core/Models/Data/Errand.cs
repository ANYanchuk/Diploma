#pragma warning disable CS8618

using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using TaskManager.Core.Models.Shared;

namespace TaskManager.Core.Models.Data;

public class Errand
{
    public Errand()
    {
        Started = DateTime.Now;
    }

    public uint Id { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }
    public string CategoryName { get; set; } = "Інше";
    public Category Category { get; set; }
    public string ReportFormatName { get; set; } = "Файл";
    public ReportFormat ReportFormat { get; set; }
    public DateTime Started { get; private set; }
    public DateTime? Deadline { get; set; }
    public string State { get; set; }
    public int? ReportId { get; set; }
    public Report? Report { get; set; }
    public ICollection<ApplicationUser> Users { get; set; }
    // Not needed
    // public Priority Priority { get; set; }
    // public uint? LeaderId { get; set; }
    // public ApplicationUser? Leader { get; set; }
}
