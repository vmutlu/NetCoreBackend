using Apsiyon.Entities.Concrete;
using Apsiyon.Utilities.Results;
using System.Threading.Tasks;
using Apsiyon.Entities;

namespace Apsiyon.Business.Abstract
{
    public interface IOperationClaimService 
    {
        Task<PagingResult<OperationClaim>> GetAllAsync(GeneralFilter generalFilter = null);
        Task<IDataResult<OperationClaim>> GetByIdAsync(int id);
        Task<IResult> AddAsync(OperationClaim tEntity);
        Task<IResult> UpdateAsync(OperationClaim tEntity);
        Task<IResult> DeleteAsync(int id);
    }
}
