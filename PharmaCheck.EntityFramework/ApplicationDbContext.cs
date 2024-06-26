﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<ProductSuppliesEntity> ProductSupplies { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<CheckEntity> Checks { get; set; }
        public DbSet<ProductCheckEntity> ProductChecks { get; set; }
        public DbSet<PharmacyProductsEntity> PharmacyProducts { get; set; }

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
            modelBuilder.ApplyConfiguration(new ProductSuppliesConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new CheckConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCheckConfiguration());
            modelBuilder.ApplyConfiguration(new PharmacyProductsConfiguration());
        }
    }
}
