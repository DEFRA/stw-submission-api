using STW.SubmissionApi.Presentation.Extensions;
using STW.SubmissionApi.Presentation.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddHealthChecks();

builder.Services.RegisterAzureServiceBus(builder.Configuration);

builder.Services.RegisterServices();

builder.Services.RegisterOptions(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapHealthChecks(
    "/health",
    HealthCheckOptionsBuilder.Build(builder.Configuration.GetValue<string>("HealthCheck:Version")));

app.Run();
