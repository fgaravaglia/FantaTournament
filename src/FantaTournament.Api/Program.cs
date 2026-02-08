using Serilog;
using FantaTournament.Api.Extensions;

// Initialize Serilog first to catch startup errors
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/api-.log", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Use Serilog as the primary logger
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext());

    // Clean registration via extension methods
    builder.Services.AddApiServices(builder.Configuration);
    builder.Services.AddApiSwagger();
    builder.Services.AddApiHealthChecks();

    var app = builder.Build();

    // Configure the middleware pipeline
    app.ConfigureApiMiddleware();

    Log.Information("FantaTournament API started successfully on Environment {Environment}", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
    app.Run();
}
catch (Exception ex) when (ex.GetType().Name is not "StopTheHostException") // Filter out Host termination exception
{
    Log.Fatal(ex, "Application startup failed");
}
finally
{
    Log.CloseAndFlush();
}
