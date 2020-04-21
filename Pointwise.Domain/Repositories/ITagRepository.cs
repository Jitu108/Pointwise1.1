using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Models;
using System.Collections.Generic;

namespace Pointwise.Domain.Repositories
{
    public interface ITagRepository : IRepository<ITag, Tag>
    {
        ITag GetByName(string name);
        IEnumerable<ITag> GetByName(IEnumerable<string> names);
    }
}
