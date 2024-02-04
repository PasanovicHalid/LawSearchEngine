using LawSearchEngine.Application.Common.Repositories.Authentification;
using LawSearchEngine.Domain.Entities;

namespace LawSearchEngine.Persistance.Repositories.Authentification
{
    public class RoleRepository : IRoleRepository
    {
        public Task<Role> AddPermissionsToRoleAsync(uint roleId, IEnumerable<uint> permissions)
        {
            throw new NotImplementedException();
        }

        public Task<Role> CreateRoleAsync(Role role)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRoleAsync(uint roleId)
        {
            throw new NotImplementedException();
        }

        public Task<Role?> GetRoleByIdAsync(uint roleId)
        {
            throw new NotImplementedException();
        }

        public Task<Role?> GetRoleByNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetRolesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetRolesByUserIdAsync(ulong userId)
        {
            throw new NotImplementedException();
        }

        public Task<Role> RemovePermissionsFromRoleAsync(uint roleId, IEnumerable<uint> permissions)
        {
            throw new NotImplementedException();
        }

        public Task<Role> UpdateRoleAsync(Role role)
        {
            throw new NotImplementedException();
        }
    }
}
