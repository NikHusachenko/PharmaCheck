using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaCheck.Database.Entities;

namespace PharmaCheck.EntityFramework.Configurations;

public sealed class SupplyConfiguration : IEntityTypeConfiguration<SupplyEntity>
{
    public void Configure(EntityTypeBuilder<SupplyEntity> builder)
    {
        builder.ToTable("Supplies").HasKey(supply => supply.Id);

        builder.HasOne<SupplierEntity>(supply => supply.Supplier)
            .WithMany(supplier => supplier.Supplies)
            .HasForeignKey(supply => supply.SupplierId);

        builder.HasOne<PharmacyEntity>(supply => supply.Pharmacy)
            .WithMany(pharmacy => pharmacy.Supplies)
            .HasForeignKey(supply => supply.PharmacyId);
    }
}