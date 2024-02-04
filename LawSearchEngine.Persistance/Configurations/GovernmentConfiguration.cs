using LawSearchEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawSearchEngine.Persistance.Configurations
{
    internal class GovernmentConfiguration : IEntityTypeConfiguration<Government>
    {
        public void Configure(EntityTypeBuilder<Government> builder)
        {
            builder.HasQueryFilter(x => !x.Deleted);
        }
    }
}
