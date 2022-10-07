using System.ComponentModel.DataAnnotations;

namespace TaskManager.ViewModels;

public class ProfileViewModel
{
    public ProfileViewModel(string role, string email, string firstName)
    {
        Role = role;
        Email = email;
        FirstName = firstName;
    }

    public uint? Id { get; set; }
    [Required]
    public string Role { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }

    public string? PhoneNumber { get; set; }
}