using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaCheck.Database.Entities;

namespace PharmaCheck.EntityFramework.Configurations;

public class PharmacyConfiguration : IEntityTypeConfiguration<PharmacyEntity>
{
    public void Configure(EntityTypeBuilder<PharmacyEntity> builder)
    {
        builder.ToTable("Pharmacies").HasKey(pharmacy => pharmacy.Id);
    }
}