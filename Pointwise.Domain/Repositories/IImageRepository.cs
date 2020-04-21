using Pointwise.Domain.Interfaces;
using System.Collections.Generic;
using Pointwise.Domain.Enums;
using Pointwise.Domain.Models;

namespace Pointwise.Domain.Repositories
{
    public interface IImageRepository : IRepository<IImage, Image>
    {
        IEnumerable<IImage> AddRange(IEnumerable<Image> images, int? articleId);
        IImage Update(IImage entity);
    }
}
