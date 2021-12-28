using LunarToggle.FeatureToggle;
using Microsoft.Extensions.DependencyInjection;

namespace LunarToggle.RedisClient
{
    public static class RedisClientExtension
    {
        public static IServiceCollection AddFeatToggleClient(this IServiceCollection service, string url, double cacheExpiration = 30) =>
            service
                .AddStackExchangeRedisCache(option =>
                    {
                        option.Configuration = url;
                    })
                .AddConcreteFeatureClient<RedisFeatToggleClient>(cacheExpiration);

    }
}