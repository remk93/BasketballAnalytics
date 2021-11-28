namespace BA.Migration
{
    public class InjectionExtension
    {
        public static IServiceCollection AddMigrationsDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentMigratorCore()
                .ConfigureRunner(config => config
                    .AddSqlServer()
                    .WithGlobalConnectionString(configuration.GetConnectionString("MSSQL"))
                    .ScanIn(typeof(DependencyInjection).Assembly).For.Migrations())
                    .AddLogging(lb => lb.AddFluentMigratorConsole())
                    .Configure<RunnerOptions>(opt => {
                        opt.Tags = new[] { "Statistics" };
                    })
                    .BuildServiceProvider(false);
            ;

            return services;
        }
    }
}
