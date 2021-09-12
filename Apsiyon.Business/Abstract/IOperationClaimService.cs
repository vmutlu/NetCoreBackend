using Apsiyon.Entities.Concrete;
using Apsiyon.Utilities.Results;
using System.Threading.Tasks;

namespace Apsiyon.Business.Abstract
{
    public interface IOperationClaimService 
    {
        Task<IDataResult<PaginationDataResult<OperationClaim>>> GetAllAsync(PaginationQuery paginationQuery = null);
        Task<IDataResult<OperationClaim>> GetByIdAsync(int id);
        Task<IResult> AddAsync(OperationClaim tEntity);
        Task<IResult> UpdateAsync(OperationClaim tEntity);
        Task<IResult> DeleteAsync(int id);
    }
}
