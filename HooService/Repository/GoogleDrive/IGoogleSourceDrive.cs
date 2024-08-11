namespace HooService.Repository.GoogleDrive
{
    public interface IGoogleSourceDrive
    {
        public IEnumerable<Google.Apis.Drive.v3.Data.File> GetFiles(string folder);
    }
}
