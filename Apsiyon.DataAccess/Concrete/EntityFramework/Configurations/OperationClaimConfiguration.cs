using Apsiyon.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apsiyon.DataAccess.Concrete.EntityFramework.Configurations
{
    public class OperationClaimConfiguration : BaseConfiguration<OperationClaim>
    {
        public override void Configure(EntityTypeBuilder<OperationClaim> modelBuilder)
        {
            modelBuilder.ToTable("OperationClaims");
            modelBuilder.Property<string>(x => x.Name).HasColumnName(@"Name").IsRequired(true).ValueGeneratedNever();

            base.Configure(modelBuilder);
        }
    }
}
