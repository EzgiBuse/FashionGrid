using FashionGrid.ProductCatalogService.Services.IServices;
using FashionGrid.ProductCatalogService.Services;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var mongoDbSettings = builder.Configuration.GetSection("MongoDB");


builder.Services.AddSingleton<IMongoClient>(_ =>
    new MongoClient(mongoDbSettings["ConnectionString"]));

builder.Services.AddSingleton<IProductService, ProductService>();


// Add services to the container.
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetSection("RedisCache:ConnectionString").Value;
});

// Continue with the rest of your setup...


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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
