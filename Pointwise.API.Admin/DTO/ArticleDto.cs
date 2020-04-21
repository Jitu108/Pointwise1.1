using Pointwise.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pointwise.API.Admin.DTO
{
    public class ArticleDto
    {
        public int ArticleId { get; set; }
        public string ArticleAuthor { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleSummary { get; set; }
        // Article Url
        public string ArticleUrl { get; set; }
        public DateTime? ArticlePublicationDate { get; set; }
        public string ArticleContent { get; set; }
        public string ArticleSynopsis { get; set; }
        public int ArticleSourceId { get; set; }
        public string ArticleSource { get; set; }

        public int ArticleCategoryId { get; set; }
        public string ArticleCategory { get; set; }
        public string ArticleAssetType { get; set; }
        public IList<string> ArticleTags { get; set; }
        public bool ArticleIsDeleted { get; set; }

        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public string ImageContentType { get; set; }
        public string ImageData { get; set; }
        public string ImageExtension { get; set; }
        public string ImageSavedTo { get; set; }
    }
}
