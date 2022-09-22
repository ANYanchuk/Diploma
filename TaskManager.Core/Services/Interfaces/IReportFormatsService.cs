using TaskManager.Core.Models;

namespace TaskManager.Core.Services
{
    public interface IReportFormatsService
    {
        public ServiceResponse<IEnumerable<string>> GetAll();
        public ServiceResponse<string> Add(string format);
        public ServiceResponse<string> Delete(string format);
    }
}