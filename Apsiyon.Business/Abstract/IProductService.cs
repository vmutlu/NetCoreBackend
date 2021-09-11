using Apsiyon.Entities.Concrete;
using Apsiyon.Utilities.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Apsiyon.Business.Abstract
{
    public interface IProductService
    {
        Task<IDataResult<Product>> GetById(int productId);
        Task<IDataResult<List<Product>>> GetList();
        Task<IDataResult<List<Product>>> GetListProductCategory(int categoryId);
        Task<IResult> Add(Product product);
        Task<IResult> Update(Product product);
        Task<IResult> Delete(Product product);
    }
}
