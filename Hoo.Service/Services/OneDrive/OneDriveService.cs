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
        private readonly IOneDriveRepository _oneDriveRepository;

        public OneDriveService(IConfiguration configuration, ILogger<OneDriveService> logger, IOneDriveRepository oneDriveRepository)
        {
            _driveId = configuration.GetValue<string>("DriveId");
            _graphClientClient = new GraphServiceClient(new SimpleAuthProvider(configuration.GetValue<string>("BearerToken")));

            _logger = logger;
            _oneDriveRepository = oneDriveRepository;
        }

        public async Task<IEnumerable<OneDriveFileItem>> GetFilesAsync()
        {
            return await _oneDriveRepository.GetFilesAsync();
        }

        public async Task SyncRemoteAsync()
        {
            var visited = new HashSet<string>();

            var directoryQueue = new Queue<string>();
            directoryQueue.Enqueue("root");

            while (directoryQueue.Count > 0)
            {
                var itemId = directoryQueue.Dequeue();


                var result = await _graphClientClient.Drives[_driveId].Items[itemId].Delta.GetAsDeltaGetResponseAsync();

                // We go by each page
                while (result.OdataNextLink != null)
                {
                    foreach (var item in result.Value)
                    {
                        if (item.File != null)
                        {
                            _oneDriveRepository.AddFileAsync(new OneDriveFileItem
                            {
                                Id = Guid.NewGuid(),
                                OneDriveId = item.Id,
                                Name = item.Name
                            });
                        }
                        
                        if (item.Folder != null && !visited.Contains(item.Id)) 
                        {
                            visited.Add(item.Id);
                            directoryQueue.Enqueue(item.Id);
                            _logger.LogInformation("Adding '" + item.Name + "' to queue");

                        }
                    }

                    result = await new Microsoft.Graph.Drives.Item.Items.Item.Delta.DeltaRequestBuilder(result.OdataNextLink, _graphClientClient.RequestAdapter).GetAsDeltaGetResponseAsync();
                }
            }
        }

        public async Task<IActionResult> ClearCacheAsync()
        {
            await _oneDriveRepository.DeleteAllFilesAsync();

            return new OkResult();
        }
    }
}

