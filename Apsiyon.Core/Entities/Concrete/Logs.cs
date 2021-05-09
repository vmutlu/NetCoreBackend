using Apsiyon.Core.Entities.Abstract;
using System;

namespace Apsiyon.Core.Entities.Concrete
{
    public class Logs : IEntity
    {
        public int Id { get; set; }
        public string Detail { get; set; }
        public DateTime Date { get; set; }
        public string Audit { get; set; }
    }
}
