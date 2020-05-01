using Pointwise.Domain.Enums;
using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Models;
using Pointwise.SqlDataAccess.Interfaces;

namespace Pointwise.SqlDataAccess.Models
{
    public partial class Image : BaseEntity, IImage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public string Path { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public Extension Extension { get; set; }
        public ImageSaveTo SavedTo { get; set; }
        public Article Article { get; set; }
        public int ArticleId { get; set; }
    }

    public partial class Image : IConvert<Domain.Models.Image>
    {
        public Domain.Models.Image ToDomainEntity()
        {
            return new Domain.Models.Image
            {
                Id = this.Id,
                Name = this.Name,
                Caption = this.Caption,
                Path = this.Path,
                ContentType = this.ContentType,
                Data = this.Data,
                Extension = this.Extension,
                SavedTo = this.SavedTo,
                CreatedOn = this.CreatedOn,
                LastModifiedOn = this.LastModifiedOn,
                IsDeleted = this.IsDeleted
            };
        }
    }
}
