using Apsiyon.DataAcccess.EntityFramework;
using Apsiyon.DataAccess.Abstract;
using Apsiyon.DataAccess.Concrete.EntityFramework.Context;
using Apsiyon.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace Apsiyon.DataAccess.Concrete.EntityFramework
{
    public class EfUserRepository : EfRepositoryBase<User, ApsiyonContext>, IUserRepository
    {
        public EfUserRepository(ApsiyonContext apsiyonContext) : base(apsiyonContext)
        {

        }

        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new ApsiyonContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };

                return result.ToList();
            }
        }
    }
}
