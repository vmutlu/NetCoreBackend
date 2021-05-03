using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Apsiyon.Core.Entities.Concrete;
using System.IO;
using Apsiyon.Entities.Concrete;

namespace Apsiyon.DataAccess.Concrete.EntityFramework.Context
{
    public class ApsiyonContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("ApsiyonConnection"));
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
    }
}
