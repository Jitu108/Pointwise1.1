using System;
using System.Collections.Generic;
using System.Linq;
using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Models;
using Pointwise.Domain.Repositories;
using Pointwise.Domain.ServiceInterfaces;

namespace Pointwise.Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository repository;
        public CategoryService(ICategoryRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public IEnumerable<ICategory> GetCategories()
        {
            return repository.GetAll().Where(x => !x.IsDeleted);
        }

        public IEnumerable<ICategory> GetCategoriesAll()
        {
            return repository.GetAll();
        }

        public IEnumerable<ICategory> GetBySearchString(string searchString)
        {
            return this.GetCategories().Where(x => x.Name.Contains(searchString));
        }

        public ICategory GetById(int id)
        {
            return repository.GetById(id);
        }

        public ICategory Add(Category entity)
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

        public ICategory Update(Category entity)
        {
            return repository.Update(entity);
        }

        public bool Exist(string name)
        {
            return repository.Exist(name);
        }
    }
}
