using ActivityTracker.Application.Interfaces;
using ActivityTracker.Infrastructure.GeoIPService;
using ActivityTracker.Infrastructure.MongoDb;
using ActivityTracker.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<MongoDbContext>();


// Add services to the container.
builder.Services.AddHttpClient<GeolocationService>();
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<IGeoLocationService, GeoLocationService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
