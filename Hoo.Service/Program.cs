using Hoo.Service.Data;
using Hoo.Service.Repository.GoogleDrive;
using Hoo.Service.Repository.OneDrive;
using Hoo.Service.Repository.WebFiles;
using Hoo.Service.Services;
using Hoo.Service.Services.GoogleDrive;
using Hoo.Service.Services.OneDrive;
using Hoo.Service.Services.WebFiles;
using HooService.Common;
using Microsoft.EntityFrameworkCore;
using Qilin.Service.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAllOrigins",
        configurePolicy: policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HooDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IWebFileRepository, WebFileRepository>();
builder.Services.AddTransient<IGoogleDriveRepository, GoogleDriveRepository>();
builder.Services.AddTransient<IOneDriveRepository, OneDriveRepository>();

builder.Services.AddTransient<IGoogleDriveService, GoogleDriveService>();
builder.Services.AddTransient<IOneDriveService, OneDriveService>();
builder.Services.AddTransient<IWebFileService, WebFileService>();

builder.Services.AddTransient<IFileProviderService, HooFileProviderService>();
builder.Services.AddTransient<IFileThumbnailProviderService, HooFileThumbnailProviderService>();

builder.Logging.ClearProviders();
builder.Logging.AddProvider(new MochiLoggerProvider());

var app = builder.Build();

app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
