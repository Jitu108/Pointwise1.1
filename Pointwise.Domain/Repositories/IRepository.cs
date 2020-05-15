using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pointwise.Domain.Repositories
{
    public interface IRepository<IEntity, TEntity> : IRepository 
        where TEntity : IEntity
    {
        IEnumerable<IEntity> GetAll();
        IEntity Add(TEntity entity);

        bool Delete(int id);

        bool SoftDelete(int id);
        bool UndoSoftDelete(int id);

        IEntity GetById(int id);

        IEntity Update(TEntity entity);

        bool Exist(string name);
    }

    public interface IRepository
    {

    }
}
