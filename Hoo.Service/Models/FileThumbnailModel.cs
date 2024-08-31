using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Hoo.Service.Models
{
    public class FileThumbnailModel
    {   
        [Required]
        public Guid FileId { get; set; }

        [Required]
        public string ThumbnailUrl { get; set; }
    }
}
