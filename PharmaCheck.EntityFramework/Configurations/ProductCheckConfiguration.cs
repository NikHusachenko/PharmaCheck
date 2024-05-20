using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaCheck.Database.Entities;

namespace PharmaCheck.EntityFramework.Configurations;

public sealed class ProductCheckConfiguration : IEntityTypeConfiguration<ProductCheckEntity>
{
    public void Configure(EntityTypeBuilder<ProductCheckEntity> builder)
    {
        builder.ToTable("Product Checks").HasKey(pc => new { pc.ProductId, pc.CheckId });

        builder.HasOne<ProductEntity>(pc => pc.Product)
            .WithMany(product => product.Checks)
            .HasForeignKey(pc => pc.ProductId);

        builder.HasOne<CheckEntity>(pc => pc.Check)
            .WithMany(check => check.Products)
            .HasForeignKey(pc => pc.CheckId);
    }
}