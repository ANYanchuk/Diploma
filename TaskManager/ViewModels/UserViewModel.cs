namespace TaskManager.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(uint id, string role, string email, string firstName)
        {
            this.Id = id;
            this.Role = role;
            this.Email = email;
            this.FirstName = firstName;

        }

        public uint Id { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public List<ErrandViewModel>? Errands { get; set; }
    }
}