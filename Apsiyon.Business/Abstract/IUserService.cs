using Apsiyon.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Apsiyon.Business.Abstract
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();
        Task<List<OperationClaim>> GetClaims(User user);
        Task Add(User user);
        Task<User> GetByEmail(string email);
    }
}
