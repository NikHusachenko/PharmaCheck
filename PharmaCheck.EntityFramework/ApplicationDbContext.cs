using Microsoft.EntityFrameworkCore;
using PharmaCheck.Database.Entities;

namespace PharmaCheck.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        //Set tables
        public DbSet<EntityBase> Entities { get; set; }
        public DbSet<MedicineEntity> MedicineEntities { get; set;}
        public DbSet<OrderEntity> OrderEntities { get; set; }
        public DbSet<PharmacyEntity> PharmacyEntities { get;set; }
        public DbSet<UserEntity> UserEntities { get; set; }

        //Tables Defined
        //Ctor/Migrations
        public ApplicationDbContext()
        {
            Database.Migrate();
        }

        //Configure db connection
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql();
        }

        //Configure tables
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EntityBase>();
            modelBuilder.Entity<MedicineEntity>();
            modelBuilder.Entity<OrderEntity>();
            modelBuilder.Entity<PharmacyEntity>();
            modelBuilder.Entity<UserEntity>();
        }

    }
}
