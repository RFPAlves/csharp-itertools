using CSharpItertools.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CSharpItertools.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIntertools(this IServiceCollection services)
        {
            services.AddSingleton<IItertools, Itertools>();
            return services;
        }
    }
}
