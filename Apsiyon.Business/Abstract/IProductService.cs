using Apsiyon.Core.Utilities.Results;
using Apsiyon.Entities.Concrete;
using System.Collections.Generic;

namespace Apsiyon.Business.Abstract
{
    public interface IProductService
    {
        IDataResult<Product> GetById(int productId);
        IDataResult<List<Product>> GetList();
        IDataResult<List<Product>> GetListProductCategory(int categoryId);
        IResult Add(Product product);
        IResult Update(Product product);
        IResult Delete(Product product);
    }
}
