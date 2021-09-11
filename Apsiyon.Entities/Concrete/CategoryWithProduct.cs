using Apsiyon.Entities.Abstract;

namespace Apsiyon.Entities.Concrete
{
    public class CategoryWithProduct : Entity
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
