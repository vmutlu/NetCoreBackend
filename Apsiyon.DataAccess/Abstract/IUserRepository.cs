using Apsiyon.Core.DataAcccess;
using Apsiyon.Core.Entities.Concrete;
using System.Collections.Generic;

namespace Apsiyon.DataAccess.Abstract
{
    public interface IUserRepository : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
    }
}
