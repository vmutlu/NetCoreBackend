using Apsiyon.DataAcccess.EntityFramework;
using Apsiyon.DataAccess.Abstract;
using Apsiyon.DataAccess.Concrete.EntityFramework.Context;
using Apsiyon.Entities.Concrete;

namespace Apsiyon.DataAccess.Concrete.EntityFramework
{
    public class EfOperationClaimRepository : EfRepositoryBase<OperationClaim, ApsiyonContext>, IOperationClaimRepository
    {
        public EfOperationClaimRepository(ApsiyonContext apsiyonContext) : base(apsiyonContext)
        {

        }
    }
}
