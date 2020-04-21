namespace Pointwise.Domain.Interfaces
{
    public interface ICategory : IBaseEntity
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
