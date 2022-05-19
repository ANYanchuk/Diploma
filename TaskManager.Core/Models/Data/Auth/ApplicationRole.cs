using System.ComponentModel.DataAnnotations;

namespace TaskManager.Core.Models.Data
{
    public class ApplicationRole
    {
        public ApplicationRole(string name)
        {
            Name = name;
        }

        [Key]
        public string Name { get; set; }
    }
}