using System.ComponentModel.DataAnnotations;

namespace TaskManager.Core.Models.Data;

public class Category
{
    public Category(string name)
    {
        Name = name;
        Errands = new();
    }
    [Key]
    public string Name { get; set; }
    public List<Errand> Errands { get; set; }
}