using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pointwise.SqlDataAccess.Interfaces
{
    public interface IConvert<DEntity>
    {
        DEntity ToDomainEntity();
    }
}
