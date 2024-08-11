using HooService.Controllers;

namespace HooService.Repository
{
    public class HooFileProvider : IFileProvider
    {
        private readonly ILogger<HooFileProvider> _logger;

        public HooFileProvider(ILogger<HooFileProvider> logger) 
        {
            _logger = logger;
        }
    }
}
