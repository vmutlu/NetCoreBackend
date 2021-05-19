using Apsiyon.Core.Entities.Abstract;
using System.Collections.Generic;

namespace Apsiyon.Entities.Concrete
{
    public class Product: IEntity
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }

        public virtual IEnumerable<CategoryWithProduct> CategoryWithProducts { get; set; }
    }
}
