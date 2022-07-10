using Basket.API.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. Dependecy injection part

// it provides IDistrbutedCache interface serves the redis implementatiton.
builder.Services.AddStackExchangeRedisCache(o => o.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString"));

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline. Middleware part
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
