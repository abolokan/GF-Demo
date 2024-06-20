using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Animals;

namespace Persistence.Configurations;

internal class PredatorlConfiguration : IEntityTypeConfiguration<Predator>
{
    public void Configure(EntityTypeBuilder<Predator> builder)
    {
        builder.HasKey(x => x.Id);
    }
}