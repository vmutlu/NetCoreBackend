using Apsiyon.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apsiyon.DataAccess.Concrete.EntityFramework.Configurations
{
    public class CategoryWithProductConfiguration : BaseConfiguration<CategoryWithProduct>
    {
        public override void Configure(EntityTypeBuilder<CategoryWithProduct> modelBuilder)
        {
            modelBuilder.ToTable("ProductCategories");
            modelBuilder.Property<int>(x => x.CategoryId).HasColumnName(@"CategoryId").IsRequired(true).UseIdentityColumn().ValueGeneratedOnAdd();
            modelBuilder.Property<int>(x => x.ProductId).HasColumnName(@"ProductId").IsRequired(true).UseIdentityColumn().ValueGeneratedOnAdd();

            modelBuilder.HasOne(x => x.Product).WithMany(r => r.CategoryWithProducts).HasForeignKey(x => x.ProductId);
            modelBuilder.HasOne(x => x.Category).WithMany(r => r.CategoryWithProducts).IsRequired(true).HasForeignKey(x => x.CategoryId);

            base.Configure(modelBuilder);
        }
    }
}
