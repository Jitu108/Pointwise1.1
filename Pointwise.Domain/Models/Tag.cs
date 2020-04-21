using Pointwise.Domain.Interfaces;

namespace Pointwise.Domain.Models
{
    public class Tag : BaseEntity, ITag
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
