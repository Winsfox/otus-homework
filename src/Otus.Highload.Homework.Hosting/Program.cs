using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Otus.Highload.Homework.Persistence.Migrations;

namespace Otus.Highload.Homework.Hosting;

public static class Program
{
    public static Task Main(string[] args) =>
        CreateHostBuilder(args)
            .Build()
            .RunWithMigrateAsync(args);

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseDefaultServiceProvider(options => options.ValidateOnBuild = true)
            .ConfigureWebHostDefaults(
                webHostBuilder =>
                    webHostBuilder
                        .UseStartup<Startup>());
}
