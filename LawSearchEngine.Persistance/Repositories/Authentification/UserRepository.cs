using LawSearchEngine.Application.Common.Repositories.Authentification;
using LawSearchEngine.Application.Common.Repositories.Authentification.Responses;
using LawSearchEngine.Domain.Entities;
using LawSearchEngine.Domain.Relationships;
using LawSearchEngine.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace LawSearchEngine.Persistance.Repositories.Authentification
{
    public class UserRepository : IUserRepository
    {
        private readonly LawSearchEngineDbContext _db;
        private readonly DbSet<User> _users;

        public UserRepository(LawSearchEngineDbContext db)
        {
            _db = db;
            _users = _db.Users;
        }

        public void AddRoleToUser(Guid userId, uint roleId)
        {
            _db.UserRoles.Add(new UserRole
            {
                UserId = userId,
                RoleId = roleId,
            });
        }

        public void CreateUser(User user)
        {
            _users.Add(user);
        }

        public Task<bool> DeleteUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByIdAsync(Guid id)
        {
            return _users.Where(u => u.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public Task<UserWitGovernmentId?> GetUserByUsernameAndPasswordAsync(string username, string password)
        {
            return _users.Where(u => u.Username == username && u.Password == password)
                .AsNoTracking()
                .Select(u => new UserWitGovernmentId
                {
                    User = u,
                })
                .FirstOrDefaultAsync();
        }

        public Task<User?> GetUserByUsernameAsync(string username)
        {
            return _users.Where(u => u.Username == username)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public void RemoveRoleFromUser(Guid userId, uint roleId)
        {
            _db.UserRoles.Remove(new UserRole
            {
                UserId = userId,
                RoleId = roleId,
            });
        }

        public void UpdateUserAsync(User user)
        {
            _users.Update(user);
        }
    }
}
