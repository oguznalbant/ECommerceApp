using Discount.API.Extensions;
using Discount.API.Repository;
using Discount.API.Repository.Abstraction;
Console.WriteLine("Discount api is started");
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
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

app.UseAuthorization();

app.MapControllers();

/* Seed Migration */

app.SeedDatabase<Program>();
Console.WriteLine("Discount api is finished");

app.Run();
