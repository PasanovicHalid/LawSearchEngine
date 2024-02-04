using LawSearchEngine.Domain.Entities;
using LawSearchEngine.Domain.Relationships;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawSearchEngine.Persistance.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(x => x.Roles)
                .WithMany()
                .UsingEntity<UserRole>();

            builder.HasQueryFilter(x => !x.Deleted);

            builder.HasData(new User
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000000"),
                Username = "admin",
                Password = "admin",
            });
        }
    }
}
