using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaCheck.Database.Entities;

namespace PharmaCheck.EntityFramework.Configurations;

internal sealed class ProductSuppliesConfiguration : IEntityTypeConfiguration<ProductSuppliesEntity>
{
    public void Configure(EntityTypeBuilder<ProductSuppliesEntity> builder)
    {
        builder.ToTable("Product Supplies").HasKey(ps => new { ps.ProductId, ps.SupplyId });

        builder.HasOne<ProductEntity>(ps => ps.Product)
            .WithMany(product => product.Supplies)
            .HasForeignKey(ps => ps.ProductId);

        builder.HasOne<SupplyEntity>(ps => ps.Supply)
            .WithMany(supply => supply.Products)
            .HasForeignKey(ps => ps.SupplyId);
    }
}