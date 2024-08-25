namespace Hoo.Service.Services.GoogleDrive.Client
{
    public interface IGoogleDriveFileCollection
    {
        IEnumerator<Google.Apis.Drive.v3.Data.File> GetEnumerator();
    }
}
