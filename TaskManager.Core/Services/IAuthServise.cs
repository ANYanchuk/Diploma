using TaskManager.Core.Models.Entities;
using TaskManager.Core.Models;

namespace TaskManager.Core.Services
{
    public interface IAuthServise
    {
        public ServiceResponse<string> Login(string email, string password);
        public ServiceResponse<string> Register(UserEntity user, string password);
        public ServiceResponse<string> ChangePassword(string email,string oldPassword, string newPassword);
    }
}