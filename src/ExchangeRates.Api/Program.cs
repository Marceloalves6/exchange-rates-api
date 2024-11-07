using ExchangeRates.Api.Application.Filters;
using ExchangeRates.Api.Application.Middlewares;
using ExchangeRates.Core;
using ExchangeRates.Core.Services;
using ExchangeRates.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Refit;
using Serilog;
using System.Reflection;

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
}).AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Exchange Rate API",
        Version = "v1",
        Description = "This application enables management of exchange rates.",
        Contact = new OpenApiContact()
        {
            Name = "Marcelo A. Cordeiro",
            Email = "marceloalves6@gmail.com",
        }

    });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddAutoMapper(typeof(ExchangeRates.Core.Startup));
builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblies(typeof(Program).Assembly, typeof(ExchangeRates.Core.Startup).Assembly));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddRefitClient<IAlphavantageService>(new RefitSettings(new NewtonsoftJsonContentSerializer())
).ConfigureHttpClient(httpClient =>
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
    app.UseSwagger(u =>
    {
        u.RouteTemplate = "swagger/{documentName}/swagger.json";
    });

    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "swagger";
        c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Exchange Rate API");
    });
}

app.UseHttpsRedirection();

app.UseMiddleware<RequestMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
