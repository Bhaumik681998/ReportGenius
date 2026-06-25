using ReportGenius.Application;
using ReportGenius.Application.Extensions;
using ReportGenius.Infrastructure.Database.Extensions;

var builder = WebApplication.CreateBuilder(args);

//---------------------------------------------------------
// Add Application Services
//---------------------------------------------------------

builder.Services.AddApplication();

//---------------------------------------------------------
// Add Infrastructure Services
//---------------------------------------------------------

builder.Services.AddInfrastructure(builder.Configuration);

//---------------------------------------------------------
// Add Framework Services
//---------------------------------------------------------

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

//---------------------------------------------------------
// Build Application
//---------------------------------------------------------

var app = builder.Build();

//---------------------------------------------------------
// Configure HTTP Request Pipeline
//---------------------------------------------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();