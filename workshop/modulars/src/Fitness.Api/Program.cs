using Fitness.Api.Passes;
using Fitness.Api.Contracts;
using Fitness.Api.Common.Events;
using Npgsql;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddEventBus();

// Add Passes module
builder.Services.AddPasses(builder.Configuration);
builder.Services.AddContracts(builder.Configuration);

// Add OpenTelemetry
const string serviceName = "demo-service";
builder.Services.AddSingleton(TracerProvider.Default.GetTracer(serviceName));

builder.Logging.AddOpenTelemetry(options =>
{
    options
        .SetResourceBuilder(
            ResourceBuilder.CreateDefault()
                .AddService(serviceName))
        .AddOtlpExporter();
});

builder.Services.AddOpenTelemetry()
      .ConfigureResource(resource => resource.AddService(serviceName))
      .WithTracing(tracing => tracing
        .AddSource(serviceName)
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddEntityFrameworkCoreInstrumentation()
        .AddRedisInstrumentation()
        .AddNpgsql()
        .AddConsoleExporter()
        .AddOtlpExporter())
      .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation()
        .AddOtlpExporter());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use module
app.UsePasses();
app.UseContracts();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Add endpoints
app.MapPasses();
app.MapContracts();

app.Run();

namespace Fitness.Api
{
    public sealed class Program;
}