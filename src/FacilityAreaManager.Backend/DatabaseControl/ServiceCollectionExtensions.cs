using DatabaseControl.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Resilience;

namespace DatabaseControl
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositoryWithResilience<Context>(this IServiceCollection services, IConfiguration configuration) where Context : DbContext
        {
            var pipelineConfiguration = configuration.GetSection(DatabaseConfiguration.REPOSITORY_RESILIENCE_PIPELINE)
                                        .Get<ResiliencePipelineConfiguration>() ?? new ResiliencePipelineConfiguration();

            services.AddResiliencePipeline(DatabaseConfiguration.REPOSITORY_RESILIENCE_PIPELINE, (builder, context) =>
            {
                ResilienceHelpers.ConfigureResiliencePipeline(builder, context, pipelineConfiguration);
            });

            services.AddSingleton<IDatabaseRepository<Context>, DatabaseRepository<Context>>();

            return services;
        }

        public static IServiceCollection AddDbContextFactory<Context>
            (this IServiceCollection services, string connectionString, string? migrationAssembly = null) where Context : DbContext
        {
            services.AddDbContextFactory<Context>(options =>
            {
                options.UseSqlServer(connectionString, b =>
                 {
                     if (!string.IsNullOrEmpty(migrationAssembly))
                     {
                         b.MigrationsAssembly(migrationAssembly);
                     }
                 });
            });

            return services;
        }
    }
}
