
using FashionGrid.CartService.Services;
using FashionGrid.CartService.Services.IServices;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var mongoDbSettings = builder.Configuration.GetSection("MongoDB");


builder.Services.AddSingleton<IMongoClient>(_ =>
    new MongoClient(mongoDbSettings["ConnectionString"]));

builder.Services.AddScoped<ICartService,CartService>();
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
