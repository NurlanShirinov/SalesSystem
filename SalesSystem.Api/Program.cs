using SalesSystem.Api.Infrastructure;
using SalesSystem.API.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

services.AddProjectDependencies(configuration);

var app = builder.Build();

app.AddPipeline(app, configuration);

app.Run();


