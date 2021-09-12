using Apsiyon.DataAcccess.EntityFramework;
using Apsiyon.DataAccess.Abstract;
using Apsiyon.DataAccess.Concrete.EntityFramework.Context;
using Apsiyon.Entities.Concrete;

namespace Apsiyon.DataAccess.Concrete.EntityFramework
{
    public class EfUserOperationClaimRepository : EfRepositoryBase<UserOperationClaim, ApsiyonContext>, IUserOperationClaimRepository
    {
        public EfUserOperationClaimRepository(ApsiyonContext apsiyonContext) : base(apsiyonContext)
        {

        }
    }
}
