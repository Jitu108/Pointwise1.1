using Pointwise.Domain.Enums;
using System;

namespace Pointwise.Domain.Interfaces
{
    public interface IUserRole
    {
        int Id { get; set; }
        IUser User { get; set; }
        EntityType EntityType { get; set; }
        public string EntityTypeName
        {
            get
            {
                return Enum.GetName(typeof(EntityType), EntityType);
            }
        }
        AccessType AccessType { get; set; }
        public string AccessTypeName
        {
            get
            {
                return Enum.GetName(typeof(AccessType), AccessType);
            }
        }
    }
}
