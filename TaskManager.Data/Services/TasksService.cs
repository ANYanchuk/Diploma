using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Models;
using TaskManager.Core.Models.Data;
using TaskManager.Core.Models.Entities;
using TaskManager.Core.Models.Shared;
using TaskManager.Core.Services;
using TaskManager.Core.Constants;

using TaskManager.Data.DbContexts;

namespace TaskManager.Data.Services;

public class TasksService : ITasksService
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    private readonly IFileStorageService fileStorage;

    public TasksService(ApplicationDbContext repository, IMapper mapper, IFileStorageService fileStorage)
    {
        this.fileStorage = fileStorage;
        context = repository;
        this.mapper = mapper;
    }

    public ServiceResponse<ErrandEntity> GetById(uint id)
    {
        Errand? task = context.Errands
            .Include(t => t.Report)
            .ThenInclude(r => r.Files)
            .AsSplitQuery()
            .Include(e => e.Users)
            .FirstOrDefault(t => t.Id == id);

        if (task is null)
            return new(false, message: ServiceResponceConstants.EntityNotFound);
        else
            return new(true, data: mapper.Map<ErrandEntity>(task));
    }

    public ServiceResponse<IEnumerable<ErrandEntity>> GetAll()
    {
        IEnumerable<Errand> tasks = context.Errands
            .Include(t => t.Report)
            .ThenInclude(r => r.Files)
            .AsSplitQuery()
            .Include(t => t.Users).ToList();

        IEnumerable<ErrandEntity> errandEntities = mapper.Map<IEnumerable<ErrandEntity>>(tasks);
        return new(true, data: mapper.Map<IEnumerable<ErrandEntity>>(errandEntities));
    }

    public ServiceResponse<IEnumerable<ErrandEntity>> GetAllForUser(uint userId)
    {
        IEnumerable<Errand> errands = context.Errands
            .Include(t => t.Report)
            .ThenInclude(r => r.Files)
            .AsSplitQuery()
            .Include(t => t.Users)
            .Where(e => e.Users.Select(u => u.Id).Contains(userId));
        IEnumerable<ErrandEntity> errandEntities = mapper.Map<IEnumerable<ErrandEntity>>(errands);
        return new(true, data: mapper.Map<IEnumerable<ErrandEntity>>(errandEntities));
    }

    public ServiceResponse<ErrandEntity> Add(ErrandEntity errandEntity)
    {
        IEnumerable<uint> ids = errandEntity.Users.Select(u => u.Id);
        IEnumerable<ApplicationUser> users = context.Users.Where(u => ids.Contains(u.Id));
        if (!users.Any())
            return new(false, TasksServiceConstants.UsersNotFound);

        if (errandEntity.Type == TaskType.Collective)
        {
            Errand errand = mapper.Map<Errand>(errandEntity);
            errand.Users = users.ToList();
            errand.State = TaskState.Opened;
            errand.Started = DateTime.Now;
            context.Errands.Add(errand);
        }
        else if (errandEntity.Type == TaskType.Individual)
        {
            DateTime now = DateTime.Now;
            IEnumerable<Errand> errands = users.Select(u =>
            {
                Errand errand = mapper.Map<Errand>(errandEntity);
                errand.Users = new List<ApplicationUser> { u };
                errand.State = TaskState.Opened;
                errand.Started = now;
                return errand;
            });
            context.Errands.AddRange(errands);
        }
        else
        {
            return new(false, TasksServiceConstants.TypeIncorrect);
        }

        int result = context.SaveChanges();
        if (result != 0)
            return new ServiceResponse<ErrandEntity>(true);
        else
            return new ServiceResponse<ErrandEntity>(false, ServiceResponceConstants.NothingChanged);
    }

    public ServiceResponse<ErrandEntity> Edit(ErrandEntity errandEntity, uint id = 0)
    {
        Errand? errand = context.Errands
            .Include(e => e.Users)
            .FirstOrDefault(e => e.Id == id);

        if (errand is null)
            return new ServiceResponse<ErrandEntity>(false, ServiceResponceConstants.EntityNotFound);

        errandEntity.Id = id;
        IEnumerable<uint> ids = errandEntity.Users.Select(u => u.Id);
        mapper.Map<ErrandEntity, Errand>(errandEntity, errand);
        var users = context.Users.Where(u => ids.Contains(u.Id)).ToList();

        foreach (var user in errand.Users)
            errand.Users.Remove(user);

        foreach (var user in users)
            errand.Users.Add(user);

        errand.State = TaskState.Opened;

        int result = context.SaveChanges();

        errand = context.Errands
            .Include(e => e.Users)
            .FirstOrDefault(e => e.Id == id);

        if (result != 0)
            return new ServiceResponse<ErrandEntity>(true, data: mapper.Map<ErrandEntity>(errand));
        else
            return new ServiceResponse<ErrandEntity>(false, ServiceResponceConstants.NothingChanged);
    }

    public ServiceResponse<string> Delete(uint id)
    {
        Errand? errand = context.Errands.FirstOrDefault(e => e.Id == id);
        if (errand is null)
            return new(false, message: ServiceResponceConstants.EntityNotFound);

        context.Errands.Remove(errand);
        context.SaveChanges();
        return new(true);
    }

    public ServiceResponse<IEnumerable<ErrandInfo>> GetInfo()
    {
        IEnumerable<Errand> errands = context.Errands
            .Include(e => e.Users)
            .Include(e => e.Report)
            .Include(e => e.ReportFormat).ToList().DistinctBy(e => e.Started);

        Dictionary<DateTime, Errand> dict = errands.ToDictionary(e => e.Started);
        Dictionary<DateTime, ErrandInfo> errandInfo =
            dict.ToDictionary(d => d.Key, e =>
                {
                    ErrandInfo errand = mapper.Map<ErrandInfo>(e.Value);
                    errand.Users = Enumerable.Empty<UserInfo>().ToList();
                    return errand;
                });

        foreach (var e in context.Errands)
        {
            foreach (ApplicationUser u in e.Users)
            {
                UserInfo user = mapper.Map<UserInfo>(u);
                user.Finished = e.Report?.LastChanged;
                errandInfo[e.Started].Users.Add(user);
            }
        }

        var a = errandInfo.Select(d => d.Value).ToList();

        return new(true, data: a);
    }
}
