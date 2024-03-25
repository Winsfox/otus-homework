using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Npgsql;
using Otus.Highload.Homework.Persistence.Migrations;
using Otus.Highload.Homework.Persistence.Migrations.Versions;
using Otus.Highload.Homework.Persistence.Options;
using Otus.Highload.Homework.Persistence.Repositories;
using Otus.Highload.Homework.Users;

namespace Otus.Highload.Homework.Hosting;

public sealed class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Options
        services
            .AddOptions<DbOptions>()
            .BindConfiguration(nameof(DbOptions));

        // Migrations
        services
            .AddFluentMigrator(typeof(InitialMigration).Assembly)
            .Configure<IOptions<DbOptions>>((options, s) => options.ConnectionString = s.Value.ConnectionString);

        // Api
        services
            .AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
            .Services
            .AddSwaggerGen();

        // Repositories
        services
            .AddScoped<IUserRepository, UserRepository>();
#pragma warning disable CS0618
        NpgsqlConnection.GlobalTypeMapper.MapEnum<Gender>("gender");
#pragma warning restore CS0618
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
