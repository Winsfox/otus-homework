using System;
using System.Reflection;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Processors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Otus.Highload.Homework.Persistence.Migrations;

public static class ServiceCollectionExtensions
{
    public static OptionsBuilder<ProcessorOptions> AddFluentMigrator(this IServiceCollection services, params Assembly[] assemblies)
    {
        services
            .AddFluentMigratorCore()
            .ConfigureRunner(
                builder => builder
                    .AddPostgres()
                    .ScanIn(assemblies).For.Migrations()
                    .ScanIn(typeof(VersionTableMetaData).Assembly).For.VersionTableMetaData());

        return services
            .AddOptions<ProcessorOptions>()
            .Configure(
                options =>
                {
                    options.ProviderSwitches = "Force Quote=false";
                    options.Timeout = TimeSpan.FromMinutes(10);
                });
    }
}
