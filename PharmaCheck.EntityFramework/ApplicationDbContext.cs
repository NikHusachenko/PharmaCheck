using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PharmaCheck.Database.Entities;
using PharmaCheck.EntityFramework.Configurations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PharmaCheck.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        //Set tables
        public DbSet<EntityBase> Bases { get; set; }
        public DbSet<MedicineEntity> Medicine { get; set;}
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<PharmacyEntity> Pharmacies { get;set; }
        public DbSet<UserEntity> Users { get; set; }

        //Tables Defined
        //Ctor/Migrations
        public ApplicationDbContext()
        {
            Database.Migrate();
        }

        //Configure db connection
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=DbPharmacy;User Id=postgres;Password=root;");
        }

        //Configure tables
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PointConfiguration());
            modelBuilder.ApplyConfiguration(new MedicineConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new PharmacyConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

    }
}
