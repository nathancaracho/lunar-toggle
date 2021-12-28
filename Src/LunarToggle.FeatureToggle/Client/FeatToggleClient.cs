using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace LunarToggle.FeatureToggle.Client
{
    public class FeatToggleClient : IFeatToggleClient
    {
        private readonly IFeatToggleClient _featToggleClient;
        private readonly IMemoryCache _memoryCache;
        private readonly double _cacheExpiration;

        public FeatToggleClient(IFeatToggleClient featToggleClient, IMemoryCache memoryCache, double cacheExpiration)
        {
            _featToggleClient = featToggleClient;
            _memoryCache = memoryCache;
            _cacheExpiration = cacheExpiration;
        }

        public Task CreateAsync(string key, bool isEnabled) =>
            _featToggleClient.CreateAsync(key, isEnabled);


        public async Task<bool> IsEnabledAsync(string key) =>
            await _memoryCache.GetOrCreateAsync(key, async cache =>
            {
                cache.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_cacheExpiration);
                return await _featToggleClient.IsEnabledAsync(key);
            });
    }
}