namespace TaskManager.ViewModels;

public class ChangePasswordViewModel
{
    public ChangePasswordViewModel(string email, string oldPassword, string newPassword)
    {
        this.Email = email;
        this.OldPassword = oldPassword;
        this.NewPassword = newPassword;
    }
    public string Email { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}