﻿using Hoo.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hoo.Service.Repository.WebFiles
{
    public interface IWebFileRepository
    {
        Task<bool> AddFileAsync(Uri fileUri);

        Task<IEnumerable<WebFileItem>> GetFilesAsync();
    }
}