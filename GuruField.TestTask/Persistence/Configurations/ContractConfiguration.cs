using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(250);
        builder.HasIndex(x => x.Name).IsUnique();

        builder.HasMany(c => c.Agreements)
            .WithOne(x => x.Contract)
            .HasForeignKey(ag => ag.ContractId);
    }
}
