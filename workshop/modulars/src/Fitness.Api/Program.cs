using Fitness.Api.Passes;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Passes module
builder.Services.AddPasses(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use passes module
app.UsePasses();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Add Passes endpoints
app.MapPasses();

app.Run();

namespace Fitness.Api
{
    public sealed class Program;
}