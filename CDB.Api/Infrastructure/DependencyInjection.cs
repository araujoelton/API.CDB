using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using CDB.Api.Services;
using CDB.Api.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace CDB.Api.Infrastructure;

public static class DependencyInjection
{
    public static IDependencyResolver CreateResolver()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IConfigProvider, ConfigProvider>();
        services.AddSingleton<ITabelaAliquotas, TabelaAliquotasIrCdb>();
        services.AddSingleton<ICdiProvider, CdiProvider>();
        services.AddSingleton<ITbProvider, TbProvider>();

        services.AddTransient<IImpostoCalculator, ImpostoCalculator>();
        services.AddTransient<ICdbCalculator, Calculator>();
        services.AddTransient<Controllers.CdbController>();

        return new ServiceProviderResolver(services.BuildServiceProvider());
    }

    private sealed class ServiceProviderResolver : IDependencyResolver
    {
        private readonly IServiceProvider _provider;
        private readonly IServiceScope _scope;

        internal ServiceProviderResolver(IServiceProvider provider)
        {
            _provider = provider;
        }

        private ServiceProviderResolver(IServiceScope scope)
        {
            _scope = scope;
            _provider = scope.ServiceProvider;
        }

        public object GetService(Type serviceType)
        {
            return _provider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var enumerableType = typeof(IEnumerable<>).MakeGenericType(serviceType);
            var services = _provider.GetService(enumerableType) as IEnumerable;
            return services?.Cast<object>() ?? Array.Empty<object>();
        }

        public IDependencyScope BeginScope()
        {
            return new ServiceProviderResolver(_provider.CreateScope());
        }

        public void Dispose()
        {
            _scope?.Dispose();
        }
    }
}
