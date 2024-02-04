using LawSearchEngine.Domain.Entities;
using LawSearchEngine.Domain.Relationships;
using Microsoft.EntityFrameworkCore;

namespace LawSearchEngine.Persistance.Context
{
    public class LawSearchEngineDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Permission> Permissions { get; set; } = null!;
        public DbSet<UserRole> UserRoles { get; set; } = null!;
        public DbSet<RolePermission> RolePermissions { get; set; } = null!;
        public DbSet<Government> Governments { get; set; } = null!;

        public LawSearchEngineDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LawSearchEngineDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
