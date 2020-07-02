using System;
using System.Collections.Generic;
using System.Text;

namespace Pointwise.Domain.Repositories
{
    public interface IExistRepository
    {
        bool Exist(string name);
    }
}
