using Apsiyon.Core.Entities.Concrete;
using System.Collections.Generic;

namespace Apsiyon.Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        void Add(User user);
        User GetByEmail(string email);
    }
}
