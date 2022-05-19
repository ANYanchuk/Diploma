#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;

namespace TaskManager.Core.Models.Data;

public class ApplicationUser
{
    public uint Id { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string PasswordHash { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }

    public string RoleName { get; set; }
    public ApplicationRole Role { get; set; }
    public ICollection<Errand> Errands { get; set; }
}
