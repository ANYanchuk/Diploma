#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TaskManager.Core.Models.Entities;

public class ReportEntity
{
    public ReportEntity(string comment, int errandId)
    {
        LastChanged = DateTime.Now;
        ErrandId = errandId;
        Comment = comment;
    }
    [Key]
    public uint Id { get; set; }

    public int ErrandId { get; set; }
    public ErrandEntity Errand { get; set; }
    public DateTime LastChanged { get; private set; }
    public List<FileEntity> Files { get; set; }
    public string? Comment { get; set; }
}
