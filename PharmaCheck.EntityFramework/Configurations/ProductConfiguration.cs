using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaCheck.Database.Entities;

namespace PharmaCheck.EntityFramework.Configurations;

public sealed class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.ToTable("Products").HasKey(product => product.Id);

        builder.HasOne<ProductTypeEntity>(product => product.ProductType)
            .WithMany(type => type.Products)
            .HasForeignKey(product => product.TypeId);
    }
}