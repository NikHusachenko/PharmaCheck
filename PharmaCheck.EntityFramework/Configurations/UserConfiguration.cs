using PharmaCheck.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PharmaCheck.EntityFramework.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users").HasKey(user => user.Id);

            builder.Property(user => user.FirstName)
                .HasMaxLength(40)
                .HasColumnName("First Name");

            builder.Property(user => user.LastName)
                .HasMaxLength(40)
                .HasColumnName("Last Name");

            builder.Property(user => user.Phone)
                .HasMaxLength(12)
                .HasColumnName("Phone Number");

            builder.Property(user => user.Orders)
                .HasColumnName("Current Orders");

        }
    }
}
