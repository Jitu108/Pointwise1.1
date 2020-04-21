using Pointwise.Domain.Interfaces;
using System;

namespace Pointwise.Domain.Models
{
    public class BaseEntity : IBaseEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? LastModifiedOn { get; set; }
    }
}
