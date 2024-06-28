using CriEduc.School.Api.Configuration;
using CriEduc.School.Api.Extensions;
using OpenTelemetry.Trace;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var appSettings = new AppSettings();
builder.Configuration.Bind(appSettings);

builder.Services.AddSingleton(appSettings);

builder.AddOpenTelemetry(appSettings);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerConfiguration();

/// Regitest all services
builder.Services.AddConfig(builder.Configuration);

// AutoMapper
//builder.Services.AddAutoMapper(typeof(AutoMapperConfiguration));

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNameCaseInsensitive= true;
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddHttpClient();

builder.Services.AddSingleton(TracerProvider.Default.GetTracer(appSettings.DistributedTracing.Jaeger.ServiceName));

var app = builder.Build();

// Configure the Prometheus scraping endpoint
app.UseOpenTelemetryPrometheusScrapingEndpoint();

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
