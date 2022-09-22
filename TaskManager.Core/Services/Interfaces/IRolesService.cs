using TaskManager.Core.Models;

namespace TaskManager.Core.Services
{
    public interface IRolesService
    {
        public ServiceResponse<IEnumerable<string>> GetAll();
        public ServiceResponse<string> Add(string role);
        public ServiceResponse<string> Delete(string role);
    }
}