using Npgsql;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Instrumentation.AspNetCore;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using OpenTelemetry.Exporter;
using Microsoft.Extensions.Options;
using CriEduc.School.Api.Configuration;
using CriEduc.School.Api.Extensions;

namespace CriEduc.School.Api.Extensions
{
    public static class OpenTelemetryExtension
    {
        public static ActivitySource ActivitySource;

        public static void AddOpenTelemetry(this WebApplicationBuilder builder, AppSettings appSettings)
        {
            var applicationName = appSettings.DistributedTracing.Jaeger.ServiceName;

            var criEducSchool = new Meter(applicationName, "1.0.0");

            var countcriEducSchool = criEducSchool.CreateCounter<int>("criEducSchool.count", description: "Counts the number of visited");

            // Custom ActivitySource for the application
            ActivitySource = new ActivitySource(applicationName, "1.0.0");

            var tracingOtlpEndpoint = builder.Configuration["OTLP_ENDPOINT_URL"];
            var otel = builder.Services.AddOpenTelemetry();

            // Configure OpenTelemetry Resources with the application name
            otel.ConfigureResource(resource => resource
                .AddService(serviceName: builder.Environment.ApplicationName)
                .AddAttributes(new Dictionary<string, object>
                {
                    ["environment.name"] = "development",
                    ["team.name"] = "backend"
                }));

            // Add Metrics for ASP.NET Core and our custom metrics and export to Prometheus
            otel.WithMetrics(metrics => metrics
                // Metrics provider from OpenTelemetry
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddMeter(criEducSchool.Name)
                // Metrics provides by ASP.NET Core in .NET 8
                .AddMeter("Microsoft.AspNetCore.Hosting")
                .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
                .AddPrometheusExporter());
            // Add Tracing for ASP.NET Core and our custom ActivitySource and export to Jaeger
            otel.WithTracing(tracing =>
            {
                tracing.AddAspNetCoreInstrumentation(x =>
                {
                    x.RecordException = true;
                });
                tracing.AddHttpClientInstrumentation();
                tracing.AddNpgsql();
                tracing.AddSource(ActivitySource.Name);
                if (tracingOtlpEndpoint != null)
                {
                    tracing.AddOtlpExporter(otlpOptions =>
                    {
                        otlpOptions.Endpoint = new Uri(tracingOtlpEndpoint);
                        otlpOptions.Protocol = OtlpExportProtocol.Grpc;
                    });
                }
                else
                {
                    tracing.AddConsoleExporter();
                }
            });

            builder.Services.AddLogging(build =>
            {                
                build.AddOpenTelemetry(options =>
                {
                    options.AddOtlpExporter(options =>
                    {
                        options.Endpoint = new Uri(tracingOtlpEndpoint);
                        options.Protocol = OtlpExportProtocol.Grpc;
                    })
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(appSettings?.DistributedTracing?.Jaeger?.ServiceName ?? string.Empty));
                    
                    options.AddProcessor(new ActivityEventExtensions()).IncludeScopes = true;                     
                    options.IncludeFormattedMessage = true;
                    options.ParseStateValues = true;

                    options.AddConsoleExporter()
                            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(appSettings?.DistributedTracing?.Jaeger?.ServiceName ?? string.Empty))
                                .AddProcessor(new ActivityEventExtensions()).IncludeScopes = true; 

                });
            });


            builder.Services.Configure<OpenTelemetryLoggerOptions>(opt =>
            {
                opt.IncludeScopes = true;
                opt.ParseStateValues = true;
                opt.IncludeFormattedMessage = true;
            });

        }

    }
}
