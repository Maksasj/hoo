using Google.Apis.Drive.v3;

namespace Hoo.Service.Services.GoogleDrive.Client
{
    public class GoogleDriveFileCollection : IGoogleDriveFileCollection
    {
        private DriveService _driveService { get; }

        public GoogleDriveFileCollection(DriveService driveService)
        {
            _driveService = driveService;
        }

        public IEnumerator<Google.Apis.Drive.v3.Data.File> GetEnumerator()
        {
            var fileList = _driveService.Files.List();
            fileList.Q = $"mimeType!='application/vnd.google-apps.folder'";
            fileList.Fields = "nextPageToken, files(id, name, size, mimeType)";

            string pageToken = null;
            do
            {
                fileList.PageToken = pageToken;

                var filesResult = fileList.Execute();
                pageToken = filesResult.NextPageToken;

                foreach (var file in filesResult.Files)
                    yield return file;

            } while (pageToken != null);
        }
    }
}
