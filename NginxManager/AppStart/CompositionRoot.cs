using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NginxManager.AppStart
{
    public static class CompositionRoot
    {
        private static readonly string[] AutoRegisterNamespaces = {"configuration", "services"};

        public static void Configure(IServiceCollection services)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();

            (from implementation in currentAssembly.DefinedTypes
             from service in implementation.ImplementedInterfaces
             where AutoRegisterNamespaces.Any(ns =>
                 // ReSharper disable once PossibleNullReferenceException because of type cannot be null in assembly and always has a namespace
                 implementation.Namespace.EndsWith(ns, StringComparison.InvariantCultureIgnoreCase))
             select new { Service = service, Implementation = implementation })
            .ToList().ForEach( registration => services.AddSingleton(registration.Service, registration.Implementation));

            services.AddSingleton<TrayApplicationContext>();
        }
    }
}