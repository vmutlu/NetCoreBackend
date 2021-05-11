using Apsiyon.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apsiyon.DataAccess.Concrete.EntityFramework.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> modelBuilder)
        {
            modelBuilder.ToTable("Products");
            modelBuilder.Property<int>(x => x.Id).HasColumnName(@"Id").IsRequired(true).UseIdentityColumn().ValueGeneratedOnAdd();
            modelBuilder.Property<string>(x => x.ProductName).HasColumnName(@"ProductName").IsRequired(true).ValueGeneratedNever();
            modelBuilder.Property<string>(x => x.QuantityPerUnit).HasColumnName(@"QuantityPerUnit").IsRequired(true).ValueGeneratedNever();
            modelBuilder.Property<decimal>(x => x.UnitPrice).HasColumnName(@"UnitPrice").IsRequired(true).ValueGeneratedNever();
            modelBuilder.Property<short>(x => x.UnitsInStock).HasColumnName(@"UnitsInStock").IsRequired(true).ValueGeneratedNever();
            modelBuilder.Property<int>(x => x.CategoryId).HasColumnName(@"CategoryId").IsRequired(true).ValueGeneratedNever();
        }
    }
}
