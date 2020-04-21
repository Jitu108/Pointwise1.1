using Pointwise.Domain.Enums;

namespace Pointwise.Domain.Interfaces
{
    public interface IImage : IBaseEntity
    {
        int Id { get; set; }
        string Name { get; set; }
        string Path { get; set; }
        string ContentType { get; set; }
        byte[] Data { get; set; }
        Extension Extension { get; set; }
        ImageSaveTo SavedTo { get; set; }
        int ArticleId { get; set; }
    }
}
