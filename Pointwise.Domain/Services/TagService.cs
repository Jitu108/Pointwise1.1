using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Models;
using Pointwise.Domain.Repositories;
using Pointwise.Domain.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pointwise.Domain.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository repository;
        public TagService(ITagRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public IEnumerable<ITag> GetTags()
        {
            return repository.GetAll().Where(x => !x.IsDeleted);
        }

        public IEnumerable<ITag> GetTagsAll()
        {
            return repository.GetAll();
        }

        public IEnumerable<ITag> GetBySearchString(string searchString)
        {
            return this.GetTags().Where(x => x.Name.Contains(searchString));
        }

        public ITag GetById(int id)
        {
            return repository.GetById(id);
        }

        public IEnumerable<ITag> GetByName(IEnumerable<string> names)
        {
            return repository.GetByName(names);
        }

        public ITag Add(Tag entity)
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

        public ITag Update(Tag entity)
        {
            return repository.Update(entity);
        }

        public bool Exist(string name)
        {
            return repository.Exist(name);
        }
    }
}
