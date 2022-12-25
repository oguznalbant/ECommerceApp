using Basket.API.Repository;
using Basket.API.Services;
using Discount.GRPC.Protos;

// Service Configurations
var builder = WebApplication.CreateBuilder(args);

// Add services to the container. Dependecy injection part

// it provides IDistrbutedCache interface serves the redis implementatiton.
builder.Services.AddStackExchangeRedisCache(o => o.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString"));

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

// consuming grpc as grpc client configuration
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(opt =>
{
    opt.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]); //discount proto service url
});

builder.Services.AddScoped<DiscountGrpcService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Middlewares
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
