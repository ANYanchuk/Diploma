#pragma warning disable CS8618

namespace TaskManager.Core.Models.Entities;

public class UserEntity
{
    public UserEntity()
    {
        Errands = new();
    }
    public uint Id { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string Role { get; set; }
    
    [Newtonsoft.Json.JsonIgnore]
    public List<ErrandEntity> Errands { get; set; }
}
