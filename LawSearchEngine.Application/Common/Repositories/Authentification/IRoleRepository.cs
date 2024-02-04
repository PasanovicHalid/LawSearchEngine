using LawSearchEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawSearchEngine.Application.Common.Repositories.Authentification
{
    public interface IRoleRepository
    {
        public Task<Role?> GetRoleByIdAsync(uint roleId);
        public Task<Role?> GetRoleByNameAsync(string roleName);
        public Task<IEnumerable<Role>> GetRolesAsync();
        public Task<IEnumerable<Role>> GetRolesByUserIdAsync(ulong userId);
        public Task<Role> CreateRoleAsync(Role role);
        public Task<Role> UpdateRoleAsync(Role role);
        public Task<bool> DeleteRoleAsync(uint roleId);
        public Task<Role> AddPermissionsToRoleAsync(uint roleId, IEnumerable<uint> permissions);
        public Task<Role> RemovePermissionsFromRoleAsync(uint roleId, IEnumerable<uint> permissions);
    }
}
