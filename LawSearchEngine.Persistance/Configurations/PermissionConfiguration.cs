using LawSearchEngine.Domain.Entities;
using LawSearchEngine.Domain.Enums;
using LawSearchEngine.Domain.Relationships;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawSearchEngine.Persistance.Configurations
{
    internal class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasMany(x => x.Roles)
                .WithMany()
                .UsingEntity<RolePermission>();

            builder.HasQueryFilter(x => !x.Deleted);

            builder.HasData(Enum.GetValues<Permissions>().Select(x => new Permission
            {
                Id = (uint)x,
                Name = x.ToString(),
            }));
        }
    }
}
