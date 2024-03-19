using PharmaCheck.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PharmaCheck.EntityFramework.Configurations
{
    public class PharmacyConfiguration : IEntityTypeConfiguration<PharmacyEntity>
    {
        public void Configure(EntityTypeBuilder<PharmacyEntity> builder)
        {

            builder.Property(pharmacy => pharmacy.Name)
                .HasMaxLength(40)
                .HasColumnName("Name");

            builder.Property(pharmacy => pharmacy.Description)
                .HasMaxLength(500)
                .HasColumnName("Description");

            builder.Property(pharmacy => pharmacy.Address)
                .HasMaxLength(60)
                .HasColumnName("Adress");

            builder.Property(pharmacy => pharmacy.Type)
                .HasMaxLength(20)
                .HasColumnName("Type");



        }
    }
}
