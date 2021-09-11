using Apsiyon.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apsiyon.DataAccess.Concrete.EntityFramework.Configurations
{
    public class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity, new()
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property<int>(x => x.Id).HasColumnName(@"Id").IsRequired(true).UseIdentityColumn().ValueGeneratedOnAdd();
        }
    }
}
