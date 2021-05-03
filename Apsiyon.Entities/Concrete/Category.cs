using Apsiyon.Entities.Abstract;

namespace Apsiyon.Entities.Concrete
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
