using FluentValidation;
using FluentValidation.AspNetCore;

namespace FacilityAreaManagerApi
{
    public static class Extenstions
    {
        public static IServiceCollection AddSharedFluentValidation(this IServiceCollection services, Type type)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining(typeof(Program));
            services.AddValidatorsFromAssemblyContaining(type);
            ValidatorOptions.Global.LanguageManager.Enabled = false;
            return services;
        }

        public static IServiceCollection AddApplicationCors(this IServiceCollection services, IConfiguration configuration, string allowSpecificOrigins, bool isDevelopment)
        {
            var allowedOriginsString = configuration[Configuration.ALLOWED_CORS_ORIGINS] ?? string.Empty;
            var allowedOrigins = allowedOriginsString.Split(",", StringSplitOptions.RemoveEmptyEntries);

            services.AddCors(options =>
            {
                options.AddPolicy(name: allowSpecificOrigins, policy =>
                {
                    policy.WithOrigins(allowedOrigins)
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .AllowAnyMethod();

                    if (isDevelopment)
                    {
                        policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
                    }
                });
            });
            return services;
        }
    }
}
