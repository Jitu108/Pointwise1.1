using System;
namespace Pointwise.Domain.Interfaces
{
    public interface IBaseEntity
    {
        bool IsDeleted { get; set; }
        int CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime? LastModifiedOn { get; set; }
    }
}
