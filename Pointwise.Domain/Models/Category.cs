using Pointwise.Domain.Interfaces;

namespace Pointwise.Domain.Models
{
    public sealed class Category : BaseEntity, ICategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
