using AutoMapper;
using TaskManager.Core.Constants;
using TaskManager.Core.Models;
using TaskManager.Core.Models.Data;
using TaskManager.Core.Models.Entities;
using TaskManager.Core.Services;
using TaskManager.Data.DbContexts;
using TaskManager.Data.Helpers;

using Microsoft.EntityFrameworkCore;

namespace TaskManager.Data.Services;

public class ReportsService : IReportsService
{
    private readonly IFileStorageService fileStorage;
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public ReportsService(IFileStorageService fileStorage, ApplicationDbContext context, IMapper mapper)
    {
        this.fileStorage = fileStorage;
        this.context = context;
        this.mapper = mapper;
    }

    public ServiceResponse<ReportEntity> Add(
        uint errandId,
        ReportEntity reportEntity,
        IEnumerable<FileEntity> files)
    {
        Report report = mapper.Map<Report>(reportEntity);
        IEnumerable<UploadedFile> uFiles = mapper.Map<IEnumerable<UploadedFile>>(files);
        foreach (UploadedFile ufile in uFiles)
        {
            ufile.Path = FilesStorageHelper.FilePath(ufile.Name);
        }
        report.Files = uFiles.ToList();
        report.ErrandId = errandId;
        context.Reports.Add(report);
        context.SaveChanges();
        fileStorage.SaveFiles(files.Select(f => (f.Content, f.Name)));
        return new(true);
    }

    public ServiceResponse<ReportEntity> Delete(uint errandId)
    {
        Errand? errand = context.Errands.Include(e => e.Report).FirstOrDefault(r => r.Id == errandId);
        if (errand is null)
            return new(false, ServiceResponceConstants.EntityNotFound);

        uint reportId = errand.Report?.Id ?? 0;

        if (reportId == 0)
            return new(false, ServiceResponceConstants.EntityNotFound);

        Report report = context.Reports.Include(r => r.Files).First(r => r.Id == reportId);

        IEnumerable<string>? filePathes = report.Files?.Select(f => f.Path);
        context.Reports.Remove(report);
        context.SaveChanges();
        if (filePathes is not null)
            fileStorage.DeleteFiles(filePathes);
        return new(true);
    }
}