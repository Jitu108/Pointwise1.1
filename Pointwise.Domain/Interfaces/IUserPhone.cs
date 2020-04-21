namespace Pointwise.Domain.Interfaces
{
    public interface IUserPhone
    {
        int Id { get; set; }
        IUser User { get; set; }
        string PhoneNumber { get; set; }
        bool IsPrimary { get; set; }
    }
}
