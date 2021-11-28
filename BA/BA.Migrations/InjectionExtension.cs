using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BA.Migrations;

public static class InjectionExtension
{
    public static IServiceCollection AddMigrationsDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentMigratorCore()
            .ConfigureRunner(config => config
                .AddSqlServer()
                .WithGlobalConnectionString(configuration.GetConnectionString("MSSQL"))
                .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .Configure<RunnerOptions>(opt => {
                    opt.Tags = new[] { "BA" };
                })
                .BuildServiceProvider(false);

        return services;
    }
}