using System;
using System.Collections.Generic;
using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Models;

namespace Pointwise.Domain.ServiceInterfaces
{
    public interface ISourceService
    {
        IEnumerable<ISource> GetSources();
        IEnumerable<ISource> GetSourcesAll();
        IEnumerable<ISource> GetBySearchString(string searchString);
        ISource GetById(int id);
        ISource Add(Models.Source entity);

        bool Delete(int id);

        bool SoftDelete(int id);
        bool UndoSoftDelete(int id);

        ISource Update(Models.Source entity);

        bool Exist(string name);
    }
}
