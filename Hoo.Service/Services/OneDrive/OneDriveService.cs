using Hoo.Service.Authetication;
using Hoo.Service.Models;
using Hoo.Service.Repository.OneDrive;
using Hoo.Service.Repository.WebFiles;
using Hoo.Service.Services.WebFiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Graph.Drives.Item.Items.Item.GetActivitiesByIntervalWithStartDateTimeWithEndDateTimeWithInterval;

namespace Hoo.Service.Services.OneDrive
{
    public class OneDriveService : IOneDriveService
    {
        private readonly string _driveId;
        private readonly GraphServiceClient _graphClientClient;

        private readonly ILogger<OneDriveService> _logger;
        private readonly IOneDriveFileRepository _oneDriveFileRepository;

        public OneDriveService(IConfiguration configuration, ILogger<OneDriveService> logger, IOneDriveFileRepository oneDriveFileRepository)
        {
            _driveId = configuration.GetValue<string>("DriveId");
            _graphClientClient = new GraphServiceClient(new SimpleAuthProvider(configuration.GetValue<string>("BearerToken")));

            _logger = logger;
            _oneDriveFileRepository = oneDriveFileRepository;
        }

        public async Task<IEnumerable<OneDriveFileItem>> GetFilesAsync()
        {
            return await _oneDriveFileRepository.GetFilesAsync();
        }

        public async Task SyncRemoteAsync()
        {
            var visited = new HashSet<string>();

            var directoryQueue = new Queue<string>();
            directoryQueue.Enqueue("root");

            while (directoryQueue.Count > 0)
            {
                var itemId = directoryQueue.Dequeue();
                visited.Add(itemId);

                var result = await _graphClientClient.Drives[_driveId].Items[itemId].Delta.GetAsDeltaGetResponseAsync();

                while (result.OdataNextLink != null)
                {
                    foreach (var item in result.Value)
                    {
                        if (item.File != null)
                        {
                            _oneDriveFileRepository.AddFileAsync(new OneDriveFileItem
                            {
                                Id = Guid.NewGuid(),
                                OneDriveId = item.Id,
                                Name = item.Name
                            });
                        }
                        else
                        {

                            if (!visited.Contains(item.Id))
                            {
                                _logger.LogInformation(item.Name);
                                directoryQueue.Enqueue(item.Id);
                            }
                        }
                    }

                    result = await new Microsoft.Graph.Drives.Item.Items.Item.Delta.DeltaRequestBuilder(result.OdataNextLink, _graphClientClient.RequestAdapter).GetAsync();
                }
            }
        }

        public async Task<IActionResult> ClearCacheAsync()
        {
            await _oneDriveFileRepository.DeleteAllFilesAsync();

            return new OkResult();
        }
    }
}

