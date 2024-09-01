using System.ComponentModel.DataAnnotations;

namespace Hoo.Service.Models
{
    public class FileItemModel
    {
        public Guid Id { get; set; }

        public string SourceType { get; set; }

        public string Name { get; set; }
    }
}
