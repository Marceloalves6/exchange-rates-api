using ExchangeRates.Core.Handlers;
using ExchangeRates.Infra;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var useInMemoryDatabase = builder.Configuration.GetValue<bool>("UseInMemoryDatabase");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(GetExchangeRateHandler));
builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblies(typeof(Program).Assembly, typeof(GetExchangeRateHandler).Assembly));
builder.Services.AddDbContext<ExchangeRatesDbContext>(options =>
{
    if (useInMemoryDatabase)
    {
        options.UseInMemoryDatabase("ExchangeDB");
    }
    else
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("ExchangeDB"));
    }
});

builder.Services.AddInfraDependencies();

if (!useInMemoryDatabase)
{
    builder.Services.ApplyMigrations();
}

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
