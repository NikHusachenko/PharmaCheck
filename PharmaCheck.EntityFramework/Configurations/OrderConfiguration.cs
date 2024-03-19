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

            builder.HasOne<UserEntity>(order => order.User)
                .WithMany(user => user.Orders)
                .HasForeignKey(order => order.UserId);
        }
    }
}
