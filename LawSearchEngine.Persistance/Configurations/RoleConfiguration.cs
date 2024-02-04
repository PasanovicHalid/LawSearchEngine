using LawSearchEngine.Domain.Entities;
using LawSearchEngine.Domain.Relationships;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawSearchEngine.Persistance.Configurations
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasMany(r => r.Permissions)
                .WithMany()
                .UsingEntity<RolePermission>();

            builder.HasQueryFilter(x => !x.Deleted);

            builder.HasMany(r => r.Users)
                .WithMany()
                .UsingEntity<UserRole>();

            builder.HasData(Role.Admin, Role.Agency);
        }
    }
}
