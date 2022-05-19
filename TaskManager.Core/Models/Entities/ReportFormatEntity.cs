using System.ComponentModel.DataAnnotations;

namespace TaskManager.Core.Models.Entities;

public class ReportFormatEntity
{
    public ReportFormatEntity(string name)
    {
        Name = name;
        Tasks = new List<ErrandEntity>();
    }
    [Key]
    public string Name { get; set; }
    public List<ErrandEntity> Tasks { get; set; }
}