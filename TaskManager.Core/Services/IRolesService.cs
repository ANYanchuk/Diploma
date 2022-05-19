using System.Threading.Tasks;
using TaskManager.Core.Models;
using TaskManager.Core.Models.Entities;

namespace TaskManager.Core.Services
{
    public interface IRolesService
    {
        public ServiceResponse<IEnumerable<string>> GetAll();
        public ServiceResponse<string> Add(string role);
        public ServiceResponse<string> Delete(string role);
    }
}