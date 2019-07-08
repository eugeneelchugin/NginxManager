using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;

namespace NginxManager.AppStart
{
    public static class CompositionRoot
    {
        private static readonly string[] AutoRegisterNamespaces = {"configuration", "services"};

        private static IContainer _container;

        public static IContainer Container => _container ?? (_container = CreateContainer());

        


        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            var currentAssembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(currentAssembly)
                .Where(type => AutoRegisterNamespaces.Any(ns =>
                        type.Namespace.EndsWith(ns, StringComparison.CurrentCultureIgnoreCase)))
                .AsImplementedInterfaces();

            builder.RegisterType<TrayApplicationContext>().SingleInstance();

            return builder.Build();
        }
    }
}