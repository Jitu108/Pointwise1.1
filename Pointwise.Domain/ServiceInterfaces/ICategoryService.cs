using Pointwise.Domain.Interfaces;
using System.Collections.Generic;

namespace Pointwise.Domain.ServiceInterfaces
{
    public interface ICategoryService
    {
        IEnumerable<ICategory> GetCategories();
        IEnumerable<ICategory> GetCategoriesAll();
        IEnumerable<ICategory> GetBySearchString(string searchString);
        ICategory GetById(int id);

        ICategory Add(Models.Category entity);

        bool Delete(int id);

        bool SoftDelete(int id);
        bool UndoSoftDelete(int id);

        ICategory Update(Models.Category entity);

        bool Exist(string name);
    }
}
