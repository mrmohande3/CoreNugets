using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using HashSharpCore.Models.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace HashSharpCore.Extensions
{
    public static class AutoFacConfigExtensions
    {
        public static IServiceProvider AddAutoFacContainer(this IServiceCollection serviceCollection)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(serviceCollection);

            var assembly = typeof(IScopedDependency).Assembly;
            containerBuilder.RegisterAssemblyTypes(assembly)
                .AssignableTo<IScopedDependency>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }
    }
}
