using StoreAPI.Services;

namespace StoreAPI.Configurations
{
    public static class ServiceConfig
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<AuthService>();
            services.AddScoped(typeof(Service<>));
            return services;
        }
    }
}
