using AutoMapper;
using TaskManager.Core.Constants;
using TaskManager.Core.Models;
using TaskManager.Data.DbContexts;
using TaskManager.Data.Models;

namespace TaskManager.Core.Services;

public class ReportFormatsService : IReportFormatsService
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    public ReportFormatsService(ApplicationDbContext repository, IMapper mapper)
    {
        context = repository;
        this.mapper = mapper;
    }

    public ServiceResponse<IEnumerable<string>> GetAll()
    {
        IEnumerable<ReportFormat> formats = context.ReportFormats;
        return new(true, data: mapper.Map<IEnumerable<string>>(formats));
    }

    public ServiceResponse<string> Add(string format)
    {
        ReportFormat reportFormat = new(format);
        context.ReportFormats.Add(reportFormat);
        int result = context.SaveChanges();
        if (result != 0)
            return new ServiceResponse<string>(true);
        else
            return new ServiceResponse<string>(false, ServiceResponceConstants.NothingChanged);
    }

    public ServiceResponse<string> Delete(string format)
    {
        ReportFormat? reportFormat = context.ReportFormats.FirstOrDefault(e => e.Name == format);
        if (reportFormat is null)
            return new(false, message: ServiceResponceConstants.EntityNotFound);

        context.ReportFormats.Remove(reportFormat);
        context.SaveChanges();
        return new(true);
    }
}
