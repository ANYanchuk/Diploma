using System.ComponentModel.DataAnnotations;

namespace TaskManager.Data.Models;

public class ReportFormat
{
    public ReportFormat(string name)
    {
        Name = name;
        Errands = new();
    }
    [Key]
    public string Name { get; set; }
    public List<Errand> Errands { get; set; }
}
