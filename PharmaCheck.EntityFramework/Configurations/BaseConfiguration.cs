using PharmaCheck.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PharmaCheck.EntityFramework.Configurations
{
    public class PointConfiguration : IEntityTypeConfiguration<EntityBase>
    {
        public void Configure(EntityTypeBuilder<EntityBase> builder) 
        {
            builder.ToTable("Bases").HasKey(point => point.Id);

            builder.Property(point => point.CreatedAt)
                .HasColumnName("Creation Date")
                .HasMaxLength(20);

            builder.Property(point => point.UpdatedAt)
                .HasColumnName("Update Date")
                .HasMaxLength(20);

            builder.Property(point => point.DeletedAt)
                .HasColumnName("Deletion Date")
                .HasMaxLength(20);
        }
    }
}
