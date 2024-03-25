using System;
using System.Threading;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Otus.Highload.Homework.Persistence.Migrations;

public static class HostExtensions
{
    public static async Task RunWithMigrateAsync(
        this IHost host,
        string[] args,
        CancellationToken token = default)
    {
        if (ShouldMigrate(args, out var dryRun))
        {
            host.Migrate(dryRun);
        }
        else
        {
            await host.RunAsync(token);
        }
    }

    private static bool ShouldMigrate(string[] args, out bool dryRun)
    {
        dryRun = false;

        if (args.Length == 0 || !"migrate".Equals(args[0], StringComparison.OrdinalIgnoreCase))
            return false;

        if (args.Length > 1 && "--dryrun".Equals(args[1], StringComparison.OrdinalIgnoreCase))
            dryRun = true;

        return true;
    }

    private static void Migrate(this IHost host, bool dryRun)
    {
        using var scope = host.Services.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

        if (dryRun)
            runner.ListMigrations();
        else
            runner.MigrateUp();
    }
}
