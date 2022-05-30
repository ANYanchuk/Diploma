using TaskManager.Core.Services;
using TaskManager.Core.Models;
using TaskManager.Data.Helpers;
using static TaskManager.Data.Helpers.EnumerableExtentions;

namespace TaskManager.Data.Services;

public class FileStorageService : IFileStorageService
{
    public ServiceResponse<string> SaveFiles(IEnumerable<(byte[] content, string fileName)> files)
    {
        foreach (var file in files.OrEmptyIfNull())
        {
            string filePath = Path.Combine(FilesStorageHelper.StoragePath, Guid.NewGuid().ToString() + Path.GetExtension(file.fileName));
            using (var stream = System.IO.File.Create(filePath))
            {
                stream.Write(file.content, 0, file.content.Length);
            }
        }
        return new(true);
    }

    public ServiceResponse<string> DeleteFiles(IEnumerable<string> filePathes)
    {
        foreach (var file in filePathes.OrEmptyIfNull())
        {
            FileInfo f = new FileInfo(file);
            if (f.Exists)
                f.Delete();
        }
        return new(true);
    }
}
