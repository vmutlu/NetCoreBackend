using Apsiyon.Core.Entities.Concrete;
using System.Collections.Generic;

namespace Apsiyon.Core.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
