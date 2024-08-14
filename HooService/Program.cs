using HooService;
using HooService.Common;
using HooService.Repository.GoogleDrive;
using HooService.Repository.OneDrive;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IGoogleDriveProvider, GoogleDriveProvider>();
builder.Services.AddTransient<IOneDriveProvider, OneDriveProvider>();

builder.Services.AddTransient<IFileProvider, HooFileProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
