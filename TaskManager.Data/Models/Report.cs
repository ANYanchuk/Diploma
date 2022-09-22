#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TaskManager.Data.Models;

public class Report
{
    [Key]
    public uint Id { get; set; }
    public uint ErrandId { get; set; }
    public Errand Errand { get; set; }
    public DateTime LastChanged { get; set; }
    public ICollection<UploadedFile> Files { get; set; }
    public string? Comment { get; set; }
}
