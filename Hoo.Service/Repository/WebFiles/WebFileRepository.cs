﻿using Hoo.Service.Data;
using Hoo.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hoo.Service.Repository.WebFiles
{
    public class WebFileRepository : IWebFileRepository
    {
        private readonly ILogger<WebFileRepository> _logger;
        private readonly HooDbContext _context;

        public WebFileRepository(ILogger<WebFileRepository> logger, HooDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IEnumerable<WebFileItem>> GetFilesAsync()
        {
            return _context.WebFiles.ToArray();
        }

        public async Task<bool> AddFileAsync(Uri fileUri)
        {
            _context.WebFiles.Add(new WebFileItem
            {
                Id = Guid.NewGuid(),
                AccessUri = fileUri,
                CreatedDate = DateTimeOffset.Now,
                LastModificationDate = DateTimeOffset.Now
            });

            var saveResult = await _context.SaveChangesAsync();

            return !(saveResult == 1);
        }
    }
}