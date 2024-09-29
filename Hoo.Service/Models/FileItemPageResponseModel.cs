namespace Hoo.Service.Models
{
    public class FileItemPageResponseModel
    {
        public int PageIndex { get; set; }

        public int ItemCount { get; set; }

        public IEnumerable<FileItemModel> Files { get; set; }
    }
}
