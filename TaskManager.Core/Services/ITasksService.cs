using TaskManager.Core.Models;
using TaskManager.Core.Models.Entities;

namespace TaskManager.Core.Services;

public interface ITasksService
{
    public ServiceResponse<IEnumerable<ErrandEntity>> GetAll();
    public ServiceResponse<IEnumerable<ErrandEntity>> GetAllForUser(uint userId);
    public ServiceResponse<ErrandEntity> GetById(uint id);
    public ServiceResponse<ErrandEntity> Add(ErrandEntity task);
    public ServiceResponse<ErrandEntity> Edit(ErrandEntity task, uint id);
    public ServiceResponse<string> Delete(uint id);
    public ServiceResponse<IEnumerable<ErrandInfo>> GetInfo();
}
