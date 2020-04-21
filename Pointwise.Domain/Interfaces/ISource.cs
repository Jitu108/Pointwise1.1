namespace Pointwise.Domain.Interfaces
{
    public interface ISource : IBaseEntity
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
