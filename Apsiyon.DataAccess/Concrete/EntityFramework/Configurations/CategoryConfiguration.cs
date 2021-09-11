using Apsiyon.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apsiyon.DataAccess.Concrete.EntityFramework.Configurations
{
    public class CategoryConfiguration : BaseConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.Property<string>(x => x.Name).HasColumnName(@"Name").IsRequired(true).ValueGeneratedNever();

            base.Configure(builder);
        }
    }
}
