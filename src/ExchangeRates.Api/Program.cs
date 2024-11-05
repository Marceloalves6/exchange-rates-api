using ExchangeRates.Api.Application.Filters;
using ExchangeRates.Api.Application.Middlewares;
using ExchangeRates.Core;
using ExchangeRates.Core.Services;
using ExchangeRates.Infra;
using Microsoft.EntityFrameworkCore;
using Refit;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog((services, loggerConfiguration) => loggerConfiguration
    .ReadFrom.Configuration(builder.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console());

var useInMemoryDatabase = builder.Configuration.GetValue<bool>("UseInMemoryDatabase");

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpExceptionHandlerFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(ExchangeRates.Core.Startup));
builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblies(typeof(Program).Assembly, typeof(ExchangeRates.Core.Startup).Assembly));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddRefitClient<IAlphavantageService>().ConfigureHttpClient(httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration.GetValue<string>("AlphavantageConfiguration:Url") ?? "");
});

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



// When in memory database is enabled, it doesn't execute migrations once it is not necessary to update databse structure
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
