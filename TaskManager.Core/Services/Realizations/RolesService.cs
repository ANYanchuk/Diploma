using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data.DbContexts;
using TaskManager.Core.Constants;
using TaskManager.Data.Models;
using TaskManager.Core.Models;
using TaskManager.Core.Services;

namespace TaskManager.Data.Services;

public class RolesService : IRolesService
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    public RolesService(ApplicationDbContext repository, IMapper mapper)
    {
        context = repository;
        this.mapper = mapper;
    }

    public ServiceResponse<IEnumerable<string>> GetAll()
    {
        IEnumerable<ApplicationRole> roles = context.Roles;
        return new(true, data: mapper.Map<IEnumerable<string>>(roles));
    }

    public ServiceResponse<string> Add(string role)
    {
        ApplicationRole appRole = new(role);
        context.Roles.Add(appRole);
        int result = context.SaveChanges();
        if (result != 0)
            return new ServiceResponse<string>(true);
        else
            return new ServiceResponse<string>(false, ServiceResponceConstants.NothingChanged);
    }

    public ServiceResponse<string> Delete(string role)
    {
        ApplicationRole? appRole = context.Roles.FirstOrDefault(e => e.Name == role);
        if (appRole is null)
            return new(false, message: ServiceResponceConstants.EntityNotFound);

        context.Roles.Remove(appRole);
        context.SaveChanges();
        return new(true);
    }
}
