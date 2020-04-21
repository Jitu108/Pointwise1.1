namespace Pointwise.Domain.Interfaces
{
    public interface ITag : IBaseEntity
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
