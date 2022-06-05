using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data.DbContexts;
using TaskManager.Core.Constants;
using TaskManager.Core.Models;
using TaskManager.Core.Models.Data;
using TaskManager.Core.Models.Entities;
using TaskManager.Core.Services;

namespace TaskManager.Data.Services;

public class UsersService : IUsersService
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    public UsersService(ApplicationDbContext repository, IMapper mapper)
    {
        context = repository;
        this.mapper = mapper;
    }

    public ServiceResponse<IEnumerable<UserEntity>> GetAll()
    {
        IEnumerable<ApplicationUser> users = context.Users;
        return new(true, data: mapper.Map<IEnumerable<UserEntity>>(users));
    }

    public ServiceResponse<IEnumerable<UserEntity>> GetWithErrands()
    {
        IEnumerable<ApplicationUser> users = context.Users
            .Include(u => u.Errands)
            .ThenInclude(e => e.Report)
            .Include(u => u.Errands)
            .ThenInclude(e => e.ReportFormat);
        return new(true, data: mapper.Map<IEnumerable<UserEntity>>(users));
    }

    public ServiceResponse<UserEntity> GetById(uint id)
    {
        ApplicationUser? user = context.Users.FirstOrDefault(u => u.Id == id);
        if (user is null)
            return new(false, ServiceResponceConstants.EntityNotFound);
        return new(true, data: mapper.Map<UserEntity>(user));
    }

    public ServiceResponse<UserEntity> Edit(UserEntity userEntity, uint id)
    {
        userEntity.Id = id;
        ApplicationUser? user = context.Users.FirstOrDefault(u => u.Id == id);
        if (user is null)
            return new(false, ServiceResponceConstants.EntityNotFound);

        mapper.Map(userEntity, user);
        int result = context.SaveChanges();
        if (result != 0)
            return new(true);
        else
            return new(false, ServiceResponceConstants.NothingChanged);
    }

    public ServiceResponse<UserEntity> Delete(uint id)
    {
        ApplicationUser? user = context.Users.FirstOrDefault(u => u.Id == id);
        if (user is null)
            return new(false, message: ServiceResponceConstants.EntityNotFound);

        context.Users.Remove(user);
        context.SaveChanges();
        return new(true);
    }
}
