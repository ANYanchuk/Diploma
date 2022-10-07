namespace TaskManager.ViewModels
{
    public class RegisterViewModel
    {
        public RegisterViewModel(string email, string firstName, string password, string role)
        {
            this.Email = email;
            this.FirstName = firstName;
            this.Password = password;
            this.Role = role;

        }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string Password { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string Role { get; set; }
    }
}