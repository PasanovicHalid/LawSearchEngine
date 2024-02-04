using LawSearchEngine.Application.Common.Repositories.Authentification.Responses;
using LawSearchEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawSearchEngine.Application.Common.Repositories.Authentification
{
    public interface IUserRepository
    {
        public Task<UserWitGovernmentId?> GetUserByUsernameAndPasswordAsync(string username, string password);
        public Task<User?> GetUserByUsernameAsync(string username);
        public Task<User?> GetUserByIdAsync(Guid id);
        public void UpdateUserAsync(User user);
        public void CreateUser(User user);
        public Task<bool> DeleteUserAsync(Guid id);
        public void AddRoleToUser(Guid userId, uint roleId);
        public void RemoveRoleFromUser(Guid userId, uint roleId);

    }
}
