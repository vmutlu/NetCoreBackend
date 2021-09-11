using Apsiyon.DataAcccess.EntityFramework;
using Apsiyon.DataAccess.Abstract;
using Apsiyon.DataAccess.Concrete.EntityFramework.Context;
using Apsiyon.Entities.Concrete;

namespace Apsiyon.DataAccess.Concrete.EntityFramework
{
    public class EfCategoryRepository : EfRepositoryBase<Category, ApsiyonContext>, ICategoryRepository
    {
        public EfCategoryRepository(ApsiyonContext apsiyonContext) : base(apsiyonContext)
        {

        }
    }
}
