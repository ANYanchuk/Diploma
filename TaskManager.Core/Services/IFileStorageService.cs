using TaskManager.Core.Models;

namespace TaskManager.Core.Services
{
    public interface IFileStorageService
    {
        public ServiceResponse<string> SaveFiles(IEnumerable<(byte[] content, string fileName)> files);
        public ServiceResponse<string> DeleteFiles(IEnumerable<string> filePathes);
    }
}
