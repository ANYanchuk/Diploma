using System.Threading.Tasks;
using TaskManager.Core.Models;
using TaskManager.Core.Models.Entities;

namespace TaskManager.Core.Services
{
    public interface IReportFormatsService
    {
        public ServiceResponse<IEnumerable<string>> GetAll();
        public ServiceResponse<string> Add(string format);
        public ServiceResponse<string> Delete(string format);
    }
}