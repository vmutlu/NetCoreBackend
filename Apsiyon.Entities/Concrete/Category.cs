using Apsiyon.Entities.Abstract;
using System.Collections.Generic;

namespace Apsiyon.Entities.Concrete
{
    public class Category : Entity
    {
        public string Name { get; set; }

        public virtual IEnumerable<CategoryWithProduct> CategoryWithProducts { get; set; }
    }
}
