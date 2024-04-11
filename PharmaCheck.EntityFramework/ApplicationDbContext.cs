using Microsoft.EntityFrameworkCore;
using PharmaCheck.Database.Entities;
using PharmaCheck.EntityFramework.Configurations;

namespace PharmaCheck.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<PharmacyEntity> Pharmacies { get; set; }
        public DbSet<CategoryEntity> ProductCategories { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ProductTypeEntity> ProductTypes { get; set; }
        public DbSet<SupplierEntity> Suppliers { get; set; }
        public DbSet<SupplyEntity> Supplies { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PharmacyConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());
            modelBuilder.ApplyConfiguration(new SupplyConfiguration());
        }
    }
}
