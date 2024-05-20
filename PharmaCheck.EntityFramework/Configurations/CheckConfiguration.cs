using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaCheck.Database.Entities;

namespace PharmaCheck.EntityFramework.Configurations;

public sealed class CheckConfiguration : IEntityTypeConfiguration<CheckEntity>
{
    public void Configure(EntityTypeBuilder<CheckEntity> builder)
    {
        builder.ToTable("Checks").HasKey(check => check.Id);
    }
}