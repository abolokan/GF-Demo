using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Animals;

namespace Persistence.Configurations;

internal class AnimalConfiguration : IEntityTypeConfiguration<Animal>
{
    public void Configure(EntityTypeBuilder<Animal> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(250);
        builder.HasIndex(x => x.Name).IsUnique();

        builder.HasMany(x => x.Predators)
            .WithOne(x => x.PredatorAnimal)
            .HasForeignKey(x => x.PredatorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Preys)
            .WithOne(x => x.FavoritePrey)
            .HasForeignKey(x => x.FavoritePreyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}