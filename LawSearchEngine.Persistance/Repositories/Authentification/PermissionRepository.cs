using LawSearchEngine.Application.Common.Repositories.Authentification;
using LawSearchEngine.Domain.Entities;

namespace LawSearchEngine.Persistance.Repositories.Authentification
{
    public class PermissionRepository : IPermissionRepository
    {
        public Task<Permission> CreatePermissionAsync(Permission permission)
        {
            throw new NotImplementedException();
        }

        public Task<Permission> DeletePermissionAsync(uint id)
        {
            throw new NotImplementedException();
        }

        public Task<Permission> GetPermissionByIdAsync(uint id)
        {
            throw new NotImplementedException();
        }

        public Task<Permission> GetPermissionByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Permission> UpdatePermissionAsync(Permission permission)
        {
            throw new NotImplementedException();
        }
    }
}
