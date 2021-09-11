using Apsiyon.DataAcccess;
using Apsiyon.Entities.Concrete;
using System.Collections.Generic;

namespace Apsiyon.DataAccess.Abstract
{
    public interface IUserRepository : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
    }
}
