using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Apsiyon.Core.Entities.Concrete;
using System.IO;
using Apsiyon.Entities.Concrete;
using Apsiyon.DataAccess.Concrete.EntityFramework.Configurations;

namespace Apsiyon.DataAccess.Concrete.EntityFramework.Context
{
    public class ApsiyonContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<Logs> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new OperationClaimConfiguration());
            modelBuilder.ApplyConfiguration(new UserOperationClaimConfiguration());
        }
    }
}
