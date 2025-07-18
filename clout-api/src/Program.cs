using System.Reflection;
using clout_api;
using clout_api.Data;
using clout_api.Extensions;
using clout_api.Utilities.Json;
using Microsoft.EntityFrameworkCore;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.OpenTelemetry()
    .CreateLogger();

try
{
    string? environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var builder = WebApplication.CreateBuilder(args);


    builder.Configuration.AddUserSecrets("606618ab-ad6e-4b2a-ae7f-9f497d4a6157");

    var corsPolicy = "AllowAll";
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: corsPolicy,
            configurePolicy: policy =>
            {
                policy.WithOrigins("http://localhost:5173")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
    });

    var connectionString = builder.Configuration.GetConnectionString("postgres") ??
                           throw new InvalidOperationException("Connection string 'postgres' not found.");

    // Add services to the container.
    builder.Services.AddDependencyExtensions(connectionString);
    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new DateTimeOffsetJsonConverter());
        });
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen();
    builder.Services.AddProblemDetails();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (environmentName is HostingEnvironment.DEVELOPMENT or HostingEnvironment.DOCKER)
    {
        using var temporaryScope = app.Services.CreateScope();
        var context = temporaryScope.ServiceProvider.GetRequiredService<PostgresContext>();

        if ((await context.Database.GetPendingMigrationsAsync()).Any())
        {
            if (environmentName == HostingEnvironment.DOCKER)
            {
                Log.Information("Applying Migrations (if any are pending)");
                await context.Database.MigrateAsync();
            }
            else
            {
                Log.Warning("Your data model is out of date. Please consider running 'make update-database' at your earliest convenience");
            }
        }
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // app.UseHttpsRedirection();
    app.UseCors(corsPolicy);
    app.UseAuthentication();
    app.UseAuthorization();

    // app.UseResponseCompression();

    app.MapControllers();
    await app.RunAsync();
}
catch (Exception ex)
{
    // Log.Logger.Error(ex.ToString());
    // This is in place to prevent EF migrations from spitting out errors
    // EF migrations don't enter through the main assembly
    // We can check the executing assembly with reflrection and only log the exception
    // if it's running through the entry assembly
    if (Assembly.GetEntryAssembly() == Assembly.GetExecutingAssembly())
    {
        Log.Fatal(ex, "Application terminated unexpectedly");
    }
}
finally
{
    await Log.CloseAndFlushAsync();
}
