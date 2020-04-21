using Pointwise.Domain.Interfaces;
using System.Collections.Generic;

namespace Pointwise.Domain.ServiceInterfaces
{
    public interface ITagService
    {
        IEnumerable<ITag> GetTags();
        IEnumerable<ITag> GetTagsAll();
        IEnumerable<ITag> GetBySearchString(string searchString);
        ITag GetById(int id);
        IEnumerable<ITag> GetByName(IEnumerable<string> names);

        ITag Add(Models.Tag entity);

        bool Delete(int id);

        bool SoftDelete(int id);
        bool UndoSoftDelete(int id);

        ITag Update(Models.Tag entity);

        bool Exist(string name);
    }
}
