using Apsiyon.DataAcccess.EntityFramework;
using Apsiyon.DataAccess.Abstract;
using Apsiyon.DataAccess.Concrete.EntityFramework.Context;
using Apsiyon.Entities.Concrete;

namespace Apsiyon.DataAccess.Concrete.EntityFramework
{
    public class EfProductRepository : EfRepositoryBase<Product, ApsiyonContext>, IProductRepository
    {
        public EfProductRepository(ApsiyonContext apsiyonContext) : base(apsiyonContext)
        {

        }
    }
}
