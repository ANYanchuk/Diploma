using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data.DbContexts;
using TaskManager.Core.Models.Data;
using TaskManager.Core.Models.Entities;
using TaskManager.Core.Models;
using TaskManager.Core.Services;

namespace TaskManager.Data.Services;
public class TasksService : ITasksService
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    public TasksService(ApplicationDbContext repository, IMapper mapper)
    {
        context = repository;
        this.mapper = mapper;
    }

    public ServiceResponse<ErrandEntity> GetById(uint id)
    {
        Errand? task = context.Errands.FirstOrDefault(t => t.Id == id);
        if (task is null)
            return new(false, "ERROROV");
        else
            return new(true, data: mapper.Map<ErrandEntity>(task));
    }

    public ServiceResponse<IEnumerable<ErrandEntity>> GetAll()
    {
        IEnumerable<Errand> tasks = context.Errands
            .Include(t => t.Users)
            .Include(t => t.Users).ToList();
        IEnumerable<ErrandEntity> errandEntities = mapper.Map<IEnumerable<ErrandEntity>>(tasks);
        return new(true, data: mapper.Map<IEnumerable<ErrandEntity>>(errandEntities));
    }

    public ServiceResponse<IEnumerable<ErrandEntity>> GetAllForUser(uint userId)
    {
        IEnumerable<Errand> errands = context.Errands
            .Include(t => t.Users)
            .Include(t => t.Users).ToList().Where(e => e.Users.Select(u => u.Id).Contains(userId));
        IEnumerable<ErrandEntity> errandEntities = mapper.Map<IEnumerable<ErrandEntity>>(errands);
        return new(true, data: mapper.Map<IEnumerable<ErrandEntity>>(errandEntities));
    }

    public ServiceResponse<ErrandEntity> Add(ErrandEntity taskEntity)
    {
        IEnumerable<uint> ids = taskEntity.Users.Select(u => u.Id);
        IEnumerable<ApplicationUser> users = context.Users.Where(u => ids.Contains(u.Id));
        Errand task = mapper.Map<Errand>(taskEntity);
        task.Users = users.ToList();
        context.Errands.Add(task);
        int result = context.SaveChanges();
        if (result != 0)
            return new ServiceResponse<ErrandEntity>(true);
        else
            return new ServiceResponse<ErrandEntity>(false, "ERROROV");
    }

    public ServiceResponse<ErrandEntity> Edit(ErrandEntity taskEntity, uint id = 0)
    {
        Errand? errand = context.Errands
            .Include(e => e.Users)
            .FirstOrDefault(e => e.Id == id);

        if (errand is null)
            return new ServiceResponse<ErrandEntity>(false, "NULLOV"); ;

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
            return new ServiceResponse<ErrandEntity>(false, "NECHEGO MENIAT");
    }

    public ServiceResponse<string> Delete(uint id)
    {
        Errand? errand = context.Errands.FirstOrDefault(e => e.Id == id);
        if (errand is null)
            return new(false, message: "NULLOV");

        context.Errands.Remove(errand);
        context.SaveChanges();
        return new(true);
    }
}
