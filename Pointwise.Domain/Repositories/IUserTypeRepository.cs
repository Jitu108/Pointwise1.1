﻿using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Models;
using System.Collections.Generic;

namespace Pointwise.Domain.Repositories
{
    public interface IUserTypeRepository : IRepository<IUserType, UserType>
    {
        IUserType GetByName(string name);
    }
}
