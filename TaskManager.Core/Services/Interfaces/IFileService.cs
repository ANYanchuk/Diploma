using TaskManager.Core.Models;
using TaskManager.Core.Models.Entities;

namespace TaskManager.Core.Services;

public interface IFileService
{
    public (string Path, string Name)? GetFile(int id);
    public Stream GetErrandsDoc(DateTime since, DateTime till);
    public Stream? GetUsersDoc(DateTime since, DateTime till, uint userId);
    public Stream GetDistributionDoc(DateTime since, DateTime till);
}