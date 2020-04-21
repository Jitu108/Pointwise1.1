using Pointwise.Domain.Interfaces;
using System.Collections.Generic;
using Pointwise.Domain.Enums;
using Pointwise.Domain.Models;
using System;

namespace Pointwise.Domain.Repositories
{
    public interface IArticleRepository : IRepository<IArticle, Article>
    {
    }
}
