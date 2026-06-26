using FluentMigrator.Runner;
using FluentMigrator.Runner.VersionTableInfo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sigillum.Arcavis.Database.Migrations.Common;
using Sigillum.Arcavis.Database.Migrations.Migrations;

var builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PostgreSqlConnection")
                       ?? throw new InvalidOperationException("Connection string 'Database' not found.");

builder.Services.AddSingleton<IVersionTableMetaData, CustomVersionTableMetaData>();

builder.Services
    .AddFluentMigratorCore()
    .ConfigureRunner(runner => runner
        .AddPostgres()
        .WithGlobalConnectionString(connectionString)
        .ScanIn(typeof(Initial).Assembly).For.Migrations())
    .AddLogging(logging => logging.AddFluentMigratorConsole());

using var host = builder.Build();

using var scope = host.Services.CreateScope();

var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

runner.MigrateUp();