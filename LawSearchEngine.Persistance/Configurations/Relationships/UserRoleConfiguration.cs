using LawSearchEngine.Domain.Relationships;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawSearchEngine.Persistance.Configurations.Relationships
{
    internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserWithRole");

            builder.HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.HasData(new UserRole
            {
                RoleId = 1,
                UserId = Guid.Parse("10000000-0000-0000-0000-000000000000"),
            });
        }
    }
}
