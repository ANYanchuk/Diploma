using System.ComponentModel.DataAnnotations;

namespace TaskManager.Data.Models;

public class ApplicationRole
{
    public ApplicationRole(string name)
    {
        Name = name;
    }

    [Key]
    public string Name { get; set; }
}
