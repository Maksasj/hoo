using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Hoo.Service.Models
{
    [PrimaryKey(nameof(Id))]
    public class OneDriveFileItem
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string OneDriveId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
