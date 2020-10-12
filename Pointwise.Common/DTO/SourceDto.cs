using System;
using System.Collections.Generic;
using System.Text;

namespace Pointwise.Common.DTO
{
    public class SourceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public IList<IArticle> Articles { get; set; }
        public bool IsDeleted { get; set; }
    }
}
