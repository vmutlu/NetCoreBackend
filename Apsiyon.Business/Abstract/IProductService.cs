using Apsiyon.Entities;
using Apsiyon.Entities.Concrete;
using Apsiyon.Utilities.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Apsiyon.Business.Abstract
{
    public interface IProductService
    {
        Task<PagingResult<Product>> GetAllAsync(GeneralFilter generalFilter = null);
        Task<IDataResult<Product>> GetByIdAsync(int id);
        Task<IResult> AddAsync(Product tEntity);
        Task<IResult> UpdateAsync(Product tEntity);
        Task<IResult> DeleteAsync(int id);
        Task<IDataResult<List<Product>>> GetListProductCategory(int categoryId);
    }
}
