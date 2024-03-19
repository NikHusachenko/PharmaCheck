using PharmaCheck.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PharmaCheck.EntityFramework.Configurations
{
    public class MedicineConfiguration : IEntityTypeConfiguration<MedicineEntity>
    {
        public void Configure(EntityTypeBuilder<MedicineEntity> builder)
        {

            builder.Property(medicine => medicine.Name)
                .HasMaxLength(50)
                .HasColumnName("Medicine Name");

            builder.Property(medicine => medicine.BuyPrice)
                .HasMaxLength(10)
                .HasColumnName("Starting Price");

            builder.Property(medicine => medicine.SellCoefficient)
                .HasColumnName("Sell Coefficient");

            builder.Property(medicine => medicine.Description)
                .HasMaxLength(500)
                .HasColumnName("Description");

            builder.Property(medicine => medicine.Instruction)
                .HasMaxLength(500)
                .HasColumnName("Instruction");

            builder.Property(medicine => medicine.Type)
                .HasColumnName("Medicine Type");

            builder.Property(medicine => medicine.OrderId)
                .HasColumnName("Order ID");

            builder.Property(medicine => medicine.PharmacyId)
                .HasColumnName("Pharmacy ID");

        }
    }
}
