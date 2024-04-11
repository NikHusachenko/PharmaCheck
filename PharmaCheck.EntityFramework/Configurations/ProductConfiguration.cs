using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaCheck.Database.Entities;

namespace PharmaCheck.EntityFramework.Configurations;

public sealed class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.ToTable("Products").HasKey(product => product.Id);

        builder.HasOne<PharmacyEntity>(product => product.Pharmacy)
            .WithMany(pharmacy => pharmacy.Products)
            .HasForeignKey(product => product.PharmacyId);

        builder.HasOne<SupplyEntity>(product => product.Supply)
            .WithMany(supply => supply.Products)
            .HasForeignKey(product => product.SupplyId);

        builder.HasOne<CategoryEntity>(product => product.Category)
            .WithMany(category => category.Products)
            .HasForeignKey(product => product.CategoryId);
    }
}