using Apsiyon.Entities.Abstract;
using System.Collections.Generic;

namespace Apsiyon.Entities.Concrete
{
    public class Product: Entity
    {
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }

        public virtual IEnumerable<CategoryWithProduct> CategoryWithProducts { get; set; }
    }
}
