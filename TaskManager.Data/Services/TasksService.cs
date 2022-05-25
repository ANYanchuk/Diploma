using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Models.Data;
using TaskManager.Core.Models.Entities;
using TaskManager.Core.Models;
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
            .Include(t => t.Users);

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

    public ServiceResponse<ErrandEntity> Add(ErrandEntity taskEntity)
    {
        IEnumerable<uint> ids = taskEntity.Users.Select(u => u.Id);
        IEnumerable<ApplicationUser> users = context.Users.Where(u => ids.Contains(u.Id));
        if (!users.Any())
            return new(false, TasksServiceConstants.UsersNotFound);
        Errand task = mapper.Map<Errand>(taskEntity);
        task.Users = users.ToList();
        context.Errands.Add(task);
        int result = context.SaveChanges();
        if (result != 0)
            return new ServiceResponse<ErrandEntity>(true);
        else
            return new ServiceResponse<ErrandEntity>(false, ServiceResponceConstants.NothingChanged);
    }

    public ServiceResponse<ErrandEntity> Edit(ErrandEntity taskEntity, uint id = 0)
    {
        Errand? errand = context.Errands
            .Include(e => e.Users)
            .FirstOrDefault(e => e.Id == id);

        if (errand is null)
            return new ServiceResponse<ErrandEntity>(false, ServiceResponceConstants.EntityNotFound);

        taskEntity.Id = id;
        IEnumerable<uint> ids = taskEntity.Users.Select(u => u.Id);
        mapper.Map<ErrandEntity, Errand>(taskEntity, errand);
        var users = context.Users.Where(u => ids.Contains(u.Id)).ToList();

        foreach (var user in errand.Users)
            errand.Users.Remove(user);

        foreach (var user in users)
            errand.Users.Add(user);

        int result = context.SaveChanges();

        if (result != 0)
            return new ServiceResponse<ErrandEntity>(true);
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
}
