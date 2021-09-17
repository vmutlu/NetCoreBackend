using Apsiyon.Entities.Concrete;
using Apsiyon.Utilities.Results;
using System.Threading.Tasks;
using Apsiyon.Entities;

namespace Apsiyon.Business.Abstract
{
    public interface IUserOperationClaimService
    {
        Task<PagingResult<UserOperationClaim>> GetAllAsync(GeneralFilter generalFilter = null);
        Task<IDataResult<UserOperationClaim>> GetByIdAsync(int id);
        Task<IResult> AddAsync(UserOperationClaim tEntity);
        Task<IResult> UpdateAsync(UserOperationClaim tEntity);
        Task<IResult> DeleteAsync(int id);
    }
}
