using Pointwise.Domain.Interfaces;

namespace Pointwise.Domain.Models
{
    public sealed class Source : BaseEntity, ISource
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
