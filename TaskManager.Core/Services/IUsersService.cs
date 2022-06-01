using System.Threading.Tasks;
using TaskManager.Core.Models;
using TaskManager.Core.Models.Entities;

namespace TaskManager.Core.Services
{
    public interface IUsersService
    {
        public ServiceResponse<IEnumerable<UserEntity>> GetAll();
        public ServiceResponse<UserEntity> GetById(uint id);
        public ServiceResponse<UserEntity> Edit(UserEntity userEntity, uint id);
        public ServiceResponse<UserEntity> Delete(uint id);
    }
}

