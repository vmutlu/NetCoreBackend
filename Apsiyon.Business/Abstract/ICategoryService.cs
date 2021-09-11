using Apsiyon.Entities.Concrete;
using Apsiyon.Utilities.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Apsiyon.Business.Abstract
{
    public interface ICategoryService
    {
        Task< IDataResult<Category>> GetById(int categoryId);
        Task<IDataResult<List<Category>>> GetList();
        Task<IResult> Add(Category category);
        Task<IResult> Update(Category category);
        Task<IResult> Delete(Category category);
    }
}
