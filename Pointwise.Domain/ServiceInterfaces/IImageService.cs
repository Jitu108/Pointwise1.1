using Pointwise.Domain.Interfaces;
using System.Collections.Generic;
using System.Web;

namespace Pointwise.Domain.ServiceInterfaces
{
    public interface IImageService
    {
        string GetImageById(int id);
        //IImage SaveImage(HttpPostedFileBase postedFile);

        IImage Add(Models.Image entity);

        IEnumerable<IImage> AddRange(IEnumerable<Models.Image> entities, int articleId);
        bool Delete(int id);

        bool SoftDelete(int id);
        bool UndoSoftDelete(int id);

        IImage Update(Models.Image entity);
        IImage Update(Models.Image entity, int articleId);
    }
}
