using Microsoft.Extensions.DependencyInjection;

namespace Apsiyon.Core.Utilities.IoC
{
    public interface ICoreModule
    {
        void Load(IServiceCollection services);
    }
}
