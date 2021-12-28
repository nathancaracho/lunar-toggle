using System.Threading.Tasks;

namespace LunarToggle.FeatureToggle.Client
{
    public interface IFeatToggleClient
    {
        Task<bool> IsEnabledAsync(string key);
        Task CreateAsync(string key, bool isEnabled);
    }

}