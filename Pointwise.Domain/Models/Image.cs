using Pointwise.Domain.Enums;
using Pointwise.Domain.Interfaces;
using System;

namespace Pointwise.Domain.Models
{
    public sealed class Image : BaseEntity, IImage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public string Path { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public Extension Extension { get; set; }
        public ImageSaveTo SavedTo { get; set; } = ImageSaveTo.Database;

        public int ArticleId { get; set; }


        public static string GetImageString(Image image)
        {
            return "data:image/png;base64," + Convert.ToBase64String(image.Data, 0, image.Data.Length);
        }

        //    public static void SaveImage(HttpPostedFileBase postedFile)
        //{
        //        byte[] bytes;
        //        using (BinaryReader br = new BinaryReader(postedFile.InputStream))
        //        {
        //            bytes = br.ReadBytes(postedFile.ContentLength);
        //        }
        //        //FilesEntities entities = new FilesEntities();
        //        //entities.tblFiles.Add(
        //        var image = new Image
        //        {
        //            Name = System.IO.Path.GetFileName(postedFile.FileName),
        //            ContentType = postedFile.ContentType,
        //            Extension = FileHelper.GetExtension(postedFile.FileName),
        //            Data = bytes
        //        };
        //        //entities.SaveChanges();
        //    }

        //public static void SaveImage(HttpPostedFileBase postedFile, string path)
        //{
        //    // Extact Image File Name
        //    string fileName = System.IO.Path.GetFileName(postedFile.FileName);

        //    // Set the Image File Path
        //    string filePath = path + "/" + fileName;

        //    // Save the Image File in the Folder -- Do this in controller
        //    // postedFile.SaveAs(Server.MapPath(filePath));

        //    //Save FilePath and fileName to database
        //}
    }
}
