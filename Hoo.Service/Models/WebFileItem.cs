using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using System.ComponentModel.DataAnnotations;

namespace Hoo.Service.Models
{
    [PrimaryKey(nameof(Id))]
    public class WebFileItem
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Uri AccessUri { get; set; }

        [Required]
        public DateTimeOffset CreatedDate { get; set; }

        [Required]
        public DateTimeOffset LastModificationDate { get; set; }
    }
}
