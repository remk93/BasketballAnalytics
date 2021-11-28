using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BA.Domain;

public static class InjectionExtension
{
    public static IServiceCollection AddMigrationsDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<EntitiesContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("MSSQL"),
                    x => x.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)),
                ServiceLifetime.Transient);

        services.AddScoped<EntitiesContext>(p =>
                p.GetRequiredService<IDbContextFactory<EntitiesContext>>()
                    .CreateDbContext());

        return services;
    }
}