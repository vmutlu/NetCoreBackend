using Apsiyon.Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Apsiyon.Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
        }
    }
}
