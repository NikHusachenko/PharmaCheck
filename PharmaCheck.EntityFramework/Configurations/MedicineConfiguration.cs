using PharmaCheck.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PharmaCheck.EntityFramework.Configurations
{
    public class MedicineConfiguration : IEntityTypeConfiguration<MedicineEntity>
    {
        public void Configure(EntityTypeBuilder<MedicineEntity> builder)
        {
            builder.ToTable("Medicines").HasKey(medicine => medicine.Id);

            builder.HasOne<OrderEntity>(medicine => medicine.Order)
                .WithMany(order => order.Medicines)
                .HasForeignKey(medicine => medicine.OrderId);

            builder.HasOne<PharmacyEntity>(medicine => medicine.Pharmacy)
                .WithMany(pharmacy => pharmacy.Medicines)
                .HasForeignKey(medicine => medicine.PharmacyId);
        }
    }
}
