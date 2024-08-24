using Hoo.Service.Data;
using Hoo.Service.Repository.WebFiles;
using HooService;
using HooService.Common;
using HooService.Repository.GoogleDrive;
using HooService.Repository.OneDrive;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WebFileDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IGoogleDriveSource, GoogleDriveSource>();
builder.Services.AddTransient<IOneDriveSource, OneDriveSource>();

builder.Services.AddTransient<IWebFileRepository, WebFileRepository>();

builder.Services.AddTransient<IFileProvider, HooFileProvider>();

var app = builder.Build();

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
