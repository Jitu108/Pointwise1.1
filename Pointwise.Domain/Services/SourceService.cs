using System;
using System.Linq;
using System.Collections.Generic;
using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Models;
using Pointwise.Domain.Repositories;
using Pointwise.Domain.ServiceInterfaces;

namespace Pointwise.Domain.Services
{
    public class SourceService : ISourceService
    {
        private readonly ISourceRepository repository;
        public SourceService(ISourceRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public IEnumerable<ISource> GetSources()
        {
            return repository.GetAll().Where(x => !x.IsDeleted);
        }

        public IEnumerable<ISource> GetSourcesAll()
        {
            return repository.GetAll();
        }

        public IEnumerable<ISource> GetBySearchString(string searchString)
        {
            return this.GetSources().Where(x => x.Name.ToLower().Contains(searchString.ToLower()));
        }

        public ISource GetById(int id)
        {
            return repository.GetById(id);
        }

        public ISource Add(Source entity)
        {
            return repository.Add(entity);
        }

        public bool Delete(int id)
        {
            return repository.Delete(id);
        }

        public bool SoftDelete(int id)
        {
            return repository.SoftDelete(id);
        }

        public bool UndoSoftDelete(int id)
        {
            return repository.UndoSoftDelete(id);
        }

        public ISource Update(Source entity)
        {
            return repository.Update(entity);
        }

        public bool Exist(string name)
        {
            return repository.Exist(name);
        }
    }
}
