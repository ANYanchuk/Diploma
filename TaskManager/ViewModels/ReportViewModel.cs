using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Models.Entities;

namespace TaskManager.ViewModels;

public class ReportViewModel
{
    // public ReportViewModel(string comment, int errandId,  IEnumerable<IFormFile> files)
    // {
    //     LastChanged = DateTime.Now;
    //     ErrandId = errandId;
    //     Comment = comment;
    //     this.files = files;
    // }

    public uint Id { get; set; }
    public uint ErrandId { get; set; }
    public DateTime LastChanged { get; set; }
    public List<IFormFile>? Files { get; set; }
    public string? Comment { get; set; }
}