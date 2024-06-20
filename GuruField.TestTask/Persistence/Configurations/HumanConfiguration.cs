using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Humans;

namespace Persistence.Configurations;

internal class HumanConfiguration : IEntityTypeConfiguration<Human>
{
    public void Configure(EntityTypeBuilder<Human> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(250);
        builder.HasIndex(x => x.Name).IsUnique();

        builder.HasOne(x => x.LeastFavoriteAnimal)
            .WithMany(x => x.HumansWithLeastFavoritAnimals)
            .HasForeignKey(x => x.LeastFavoriteAnimalId);

        builder.HasOne(x => x.MostFavoriteAnimal)
            .WithMany(x => x.HumansWithMostFavoritAnimals)
            .HasForeignKey(x => x.MostFavoriteAnimalId);
    }
}