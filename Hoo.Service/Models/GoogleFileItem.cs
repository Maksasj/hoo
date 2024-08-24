using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Hoo.Service.Models
{
    [PrimaryKey(nameof(Id))]
    public class GoogleFileItem
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string GoogleId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
