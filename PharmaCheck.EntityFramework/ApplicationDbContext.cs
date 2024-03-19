using Microsoft.EntityFrameworkCore;
using PharmaCheck.Database.Entities;
using PharmaCheck.EntityFramework.Configurations;

namespace PharmaCheck.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<MedicineEntity> Medicine { get; set;}
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<PharmacyEntity> Pharmacies { get;set; }
        public DbSet<UserEntity> Users { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MedicineConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new PharmacyConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
