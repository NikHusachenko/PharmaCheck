using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaCheck.Database.Entities;

namespace PharmaCheck.EntityFramework.Configurations;

public sealed class PharmacyProductsConfiguration : IEntityTypeConfiguration<PharmacyProductsEntity>
{
    public void Configure(EntityTypeBuilder<PharmacyProductsEntity> builder)
    {
        builder.ToTable("Pharmacy Products").HasKey(pp => new { pp.PharmacyId, pp.ProductId });

        builder.HasOne<PharmacyEntity>(pp => pp.Pharmacy)
            .WithMany(pharmacy => pharmacy.Products)
            .HasForeignKey(pp => pp.PharmacyId);

        builder.HasOne<ProductEntity>(pp => pp.Product)
            .WithMany(product => product.Pharmacies)
            .HasForeignKey(pp => pp.ProductId);
    }
}