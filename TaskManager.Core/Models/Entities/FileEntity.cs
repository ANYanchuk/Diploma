using System.ComponentModel.DataAnnotations;

namespace TaskManager.Core.Models.Entities;

public class FileEntity
{
    public FileEntity(string name)
    {
        Name = name;
    }

    [Key]
    public int Id { get; set; }
    public string? Path { get; set; }
    public string Name { get; set; }
    public byte[] Content { get; set; }
    public uint? ReportId { get; set; }
}
