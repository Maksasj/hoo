namespace Hoo.Service.Models
{
    public class FileItemPageResponseModel
    {
        public int PageIndex { get; set; }

        public int ItemCount { get; set; }

        public FileItemModel[] Files { get; set; }
    }
}
