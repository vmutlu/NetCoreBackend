using Apsiyon.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apsiyon.DataAccess.Concrete.EntityFramework.Configurations
{
    public class UserOperationClaimConfiguration : BaseConfiguration<UserOperationClaim>
    {
        public override void Configure(EntityTypeBuilder<UserOperationClaim> modelBuilder)
        {
            modelBuilder.ToTable("UserOperationClaims");
            modelBuilder.Property<int>(x => x.UserId).HasColumnName(@"UserId").IsRequired(true).ValueGeneratedNever();
            modelBuilder.Property<int>(x => x.OperationClaimId).HasColumnName(@"OperationClaimId").IsRequired(true).ValueGeneratedNever();

            modelBuilder.HasOne(x => x.User).WithMany(op => op.UserOperationClaims).IsRequired(true).HasForeignKey(x => x.UserId);
            modelBuilder.HasOne(x => x.OperationClaim).WithMany(op => op.UserOperationClaims).IsRequired(true).HasForeignKey(x => x.OperationClaimId);

            base.Configure(modelBuilder);
        }
    }
}
