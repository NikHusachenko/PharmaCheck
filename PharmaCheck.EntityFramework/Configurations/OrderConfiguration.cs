using PharmaCheck.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PharmaCheck.EntityFramework.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.ToTable("Orders").HasKey(order => order.Id);

            builder.Property(order => order.User)
                .HasMaxLength(40)
                .HasColumnName("User");

            builder.Property(order => order.Medicines)
                .HasColumnName("Medicines");
        }
    }
}
