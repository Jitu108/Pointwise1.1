using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Pointwise.Domain.Helper;
using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Models;
using Pointwise.Domain.Repositories;
using Pointwise.Domain.ServiceInterfaces;

namespace Pointwise.Domain.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository repository;

        public ImageService(IImageRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public string GetImageById(int id)
        {
            var image = repository.GetById(id);
            return GetImageString((Image)image);
        }

        //public IImage SaveImage(HttpPostedFileBase postedFile)
        //{
        //    if (postedFile == null) throw new ArgumentNullException(nameof(postedFile));

        //    byte[] bytes;
        //    using (BinaryReader br = new BinaryReader(postedFile.InputStream))
        //    {
        //        bytes = br.ReadBytes(postedFile.ContentLength);
        //    }
        //    var image = new Image
        //    {
        //        Name = System.IO.Path.GetFileName(postedFile.FileName),
        //        ContentType = postedFile.ContentType,
        //        Extension = FileHelper.GetExtension(postedFile.FileName),
        //        Data = bytes
        //    };

        //    return repository.Add(image);
        //}

        public static string GetImageString(Image image)
        {
            return "data:image/png;base64," + Convert.ToBase64String(image.Data, 0, image.Data.Length);
        }

        public IImage Add(Image entity)
        {
            return repository.Add(entity);
        }

        public IEnumerable<IImage> AddRange(IEnumerable<Image> entities, int articleId)
        {
            return repository.AddRange(entities, articleId);
        }

        public bool Delete(int id)
        {
            return repository.Delete(id);
        }

        public bool SoftDelete(int id)
        {
            return repository.SoftDelete(id);
        }

        public bool UndoSoftDelete(int id)
        {
            return repository.UndoSoftDelete(id);
        }

        public IImage Update(Image entity)
        {
            return repository.Update(entity);
        }

        public IImage Update(Image entity, int articleId)
        {
            entity.ArticleId = articleId;
            return repository.Update(entity);
        }
    }
}
