using System.ComponentModel.DataAnnotations;

namespace Hoo.Service.Models
{
    public class FileThumbnailResponseModel
    {
        public Guid FileId { get; set; }

        public string ThumbnailUrl { get; set; }
    }
}
