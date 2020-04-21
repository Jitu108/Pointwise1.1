using Pointwise.Domain.Interfaces;
using System.Collections.Generic;

namespace Pointwise.API.Admin.DTO
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public IList<IArticle> Articles { get; set; }
        public bool IsDeleted { get; set; }
    }
}
