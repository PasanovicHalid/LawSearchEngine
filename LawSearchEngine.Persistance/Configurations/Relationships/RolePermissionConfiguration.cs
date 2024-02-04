using LawSearchEngine.Domain.Enums;
using LawSearchEngine.Domain.Relationships;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawSearchEngine.Persistance.Configurations.Relationships
{
    internal class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            var allPermissions = Enum.GetValues<Permissions>();

            builder.ToTable("RoleWithPermission");

            builder.HasKey(x => new { x.RoleId, x.PermissionId });

            builder.HasData(allPermissions.Select(x => new RolePermission
            {
                RoleId = 1,
                PermissionId = (uint)x,
            }));
        }
    }
}
