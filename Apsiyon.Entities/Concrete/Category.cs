using Apsiyon.Core.Entities.Abstract;
using System.Collections.Generic;

namespace Apsiyon.Entities.Concrete
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IEnumerable<CategoryWithProduct> CategoryWithProducts { get; set; }
    }
}
