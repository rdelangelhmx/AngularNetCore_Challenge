using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Server.Classes;
using Server.Features;
using Server.Helpers;
using Server.Interfaces;
using Server.Persistence;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
IConfiguration AppConfiguration = builder.Configuration;
var appConfig = AppConfiguration.GetSection("Configuration").Get<ConfigApp>();

// Add Configuration
builder.Services.Configure<ConfigApp>(AppConfiguration.GetSection("Configuration"));
builder.Services.AddSingleton(s => s.GetRequiredService<IOptions<ConfigApp>>().Value);

builder.Services.AddDbContext<CustomersContext>(options => options.UseInMemoryDatabase(databaseName: appConfig.Application.DataBase));
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();

builder.Services.AddMemoryCache();

builder.Services.AddHttpClient();
builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc($"v{appConfig.Application.Version}", new OpenApiInfo
    {
        Title = appConfig.Application.Name,
        Version = $"v{appConfig.Application.Version}",
        Description = appConfig.Application.Description,
        Contact = new OpenApiContact
        {
            Name = appConfig.Application.Company,
            Email = appConfig.Application.Email,
            Url = new Uri(appConfig.Application.WebPage),
        }
    });
    options.CustomSchemaIds(type => type.ToString());
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

    options.AddSecurityDefinition(appConfig.Application.Scheme, new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Name = "X-API-KEY",
        Description = "Authentication Token",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = appConfig.Application.Scheme }
            },
            new string[] { }
        }
    });
});

builder.Services.AddAuthentication(appConfig.Application.Scheme)
    .AddScheme<BasicAuthenticationOptions, CustomAuthenticationHandler>(appConfig.Application.Scheme, null);

builder.Services.AddHostedService<SeedData>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint($"v{appConfig.Application.Version}/swagger.json", $"{appConfig.Application.Name} v{appConfig.Application.Version}");
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.MapControllers();

app.Run();
