using System.Threading.Tasks;
using LunarToggle.FeatureToggle.Client;
using Microsoft.Extensions.Caching.Distributed;

namespace LunarToggle.RedisClient
{
    public class RedisFeatToggleClient : IFeatToggleClient
    {
        private readonly IDistributedCache _redis;

        public RedisFeatToggleClient(IDistributedCache redis) => _redis = redis;

        public async Task CreateAsync(string key, bool isEnabled) =>
           await _redis.SetStringAsync(key, isEnabled.ToString());


        public async Task<bool> IsEnabledAsync(string key)
        {
            var result = await _redis.GetStringAsync(key);
            if (result == null)
                return false;
            return bool.Parse(result);
        }
    }
}