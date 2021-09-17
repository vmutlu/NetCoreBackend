using Apsiyon.Entities;
using Apsiyon.Entities.Concrete;
using Apsiyon.Utilities.Results;
using System.Threading.Tasks;

namespace Apsiyon.Business.Abstract
{
    public interface ICategoryService
    {
        Task<PagingResult<Category>> GetAllAsync(GeneralFilter generalFilter = null);
        Task<IDataResult<Category>> GetByIdAsync(int id);
        Task<IResult> AddAsync(Category tEntity);
        Task<IResult> UpdateAsync(Category tEntity);
        Task<IResult> DeleteAsync(int id);
    }
}
