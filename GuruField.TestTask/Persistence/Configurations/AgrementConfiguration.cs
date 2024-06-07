using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal class AgrementConfiguration : IEntityTypeConfiguration<Agreement>
{
    public void Configure(EntityTypeBuilder<Agreement> builder)
    {
        builder.HasKey(x => x.Id);

        builder.OwnsOne(p => p.HourlyPrice,
            builder =>
            {
                builder.Property(x => x.Currency)
                .HasMaxLength(3)
                .HasColumnName("Currency")
                .IsRequired();

                builder.Property(x => x.Amount)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("Amount")
                .IsRequired();
            }
        );

        builder.HasMany(c => c.WorkData)
            .WithOne(x => x.Agreement)
            .HasForeignKey(ag => ag.AgreementId);
    }
}
