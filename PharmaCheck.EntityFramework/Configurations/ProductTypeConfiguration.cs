using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaCheck.Database.Entities;

namespace PharmaCheck.EntityFramework.Configurations;

internal sealed class ProductTypeConfiguration : IEntityTypeConfiguration<ProductTypeEntity>
{
    public void Configure(EntityTypeBuilder<ProductTypeEntity> builder)
    {
        builder.ToTable("Product types").HasKey(type => type.Id);

        builder.HasOne<CategoryEntity>(type => type.Category)
            .WithMany(category => category.Types)
            .HasForeignKey(type => type.CategoryId);
    }
}