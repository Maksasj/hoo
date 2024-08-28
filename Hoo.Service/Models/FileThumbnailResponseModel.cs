using System.ComponentModel.DataAnnotations;

namespace Hoo.Service.Models
{
    public class FileThumbnailResponseModel
    {
        public Guid Id { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
