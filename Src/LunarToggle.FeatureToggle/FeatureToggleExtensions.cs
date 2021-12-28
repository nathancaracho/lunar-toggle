using LunarToggle.FeatureToggle.Client;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace LunarToggle.FeatureToggle
{
    public static class FeatureToggleExtensions
    {
        public static IServiceCollection AddConcreteFeatureClient<TProvider>(this IServiceCollection services, double cacheExpiration = 30) where TProvider : class, IFeatToggleClient =>
            services
            .AddScoped<TProvider>()
            .AddMemoryCache()
            .AddScoped<IFeatToggleClient>(serviceProvider =>
            {

                var client = serviceProvider.GetRequiredService<TProvider>();
                var memoryCache = serviceProvider.GetRequiredService<IMemoryCache>();
                return new FeatToggleClient(client, memoryCache, cacheExpiration);
            });
    }
}