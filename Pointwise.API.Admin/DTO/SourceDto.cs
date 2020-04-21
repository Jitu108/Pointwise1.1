using Pointwise.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pointwise.API.Admin.DTO
{
    public class SourceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public IList<IArticle> Articles { get; set; }
        public bool IsDeleted { get; set; }
    }
}
