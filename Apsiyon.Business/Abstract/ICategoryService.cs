using Apsiyon.Core.Utilities.Results;
using Apsiyon.Entities.Concrete;
using System.Collections.Generic;

namespace Apsiyon.Business.Abstract
{
    public interface ICategoryService
    {
        IDataResult<Category> GetById(int categoryId);
        IDataResult<List<Category>> GetList();
        IResult Add(Category category);
        IResult Update(Category category);
        IResult Delete(Category category);
    }
}
