using System.ComponentModel.DataAnnotations;

namespace TaskManager.Core.Models.Data;

public class UploadedFile
{
    public UploadedFile(string path, string name)
    {
        Path = path;
        Name = name;
    }
    [Key]
    public int Id { get; set; }
    public string Path { get; set; }
    public string Name { get; set; }
    public int? AnswerId { get; set; }
}
