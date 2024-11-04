using ExchangeRates.Api.Application.Filters;
using ExchangeRates.Api.Application.Middlewares;
using ExchangeRates.Core;
using ExchangeRates.Infra;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var useInMemoryDatabase = builder.Configuration.GetValue<bool>("UseInMemoryDatabase");

// Add services to the container.
builder.Services.AddControllers(options=>
{
    options.Filters.Add<HttpExceptionHandlerFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(ExchangeRates.Core.Startup));
builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblies(typeof(Program).Assembly, typeof(ExchangeRates.Core.Startup).Assembly));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
builder.Services.AddCoreDependencies();

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

app.UseMiddleware<RequestMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
