using Apsiyon.Entities.Concrete;
using Apsiyon.Utilities.Results;
using System.Threading.Tasks;

namespace Apsiyon.Business.Abstract
{
    public interface IUserOperationClaimService
    {
        Task<IDataResult<PaginationDataResult<UserOperationClaim>>> GetAllAsync(PaginationQuery paginationQuery = null);
        Task<IDataResult<UserOperationClaim>> GetByIdAsync(int id);
        Task<IResult> AddAsync(UserOperationClaim tEntity);
        Task<IResult> UpdateAsync(UserOperationClaim tEntity);
        Task<IResult> DeleteAsync(int id);
    }
}
