using LawSearchEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawSearchEngine.Application.Common.Repositories.Authentification
{
    public interface IPermissionRepository
    {
        public Task<Permission> CreatePermissionAsync(Permission permission);
        public Task<Permission> UpdatePermissionAsync(Permission permission);
        public Task<Permission> DeletePermissionAsync(uint id);
        public Task<Permission> GetPermissionByIdAsync(uint id);
        public Task<Permission> GetPermissionByNameAsync(string name);
    }
}
