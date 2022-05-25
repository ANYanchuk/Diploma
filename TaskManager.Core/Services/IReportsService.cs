using TaskManager.Core.Models;
using TaskManager.Core.Models.Entities;

namespace TaskManager.Core.Services;

public interface IReportsService
{
    public ServiceResponse<ReportEntity> Add(
        uint errandId,
        ReportEntity report,
        IEnumerable<FileEntity> files);
    public ServiceResponse<ReportEntity> Delete(uint errandId);
}