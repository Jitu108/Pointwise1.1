namespace Pointwise.Domain.Interfaces
{
    public interface IUserEmail : IBaseEntity
    {
        int Id { get; set; }
        IUser User { get; set; }
        string EmailAddress { get; set; }
        bool IsPrimary { get; set; }
    }
}
