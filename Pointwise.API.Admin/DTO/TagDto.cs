using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pointwise.API.Admin.DTO
{
    public class TagDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
