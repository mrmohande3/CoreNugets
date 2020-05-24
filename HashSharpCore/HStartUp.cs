using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using HashSharpCore.DataLayer;
using HashSharpCore.Extensions;
using HashSharpCore.MiddleWares;
using HashSharpCore.Models;
using HashSharpCore.Models.Contracts;
using HashSharpCore.Services.Contracts;
using HashSharpCore.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HashSharpCore
{
    public class HStartUp
    {
        private SiteSettings _siteSetting;
        public static string BaseUrl;
        public HStartUp(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
            _siteSetting = Configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
            Initializer.SiteSettings = _siteSetting;
            AutoMapperConfigExtensions.ConfigurationMapper();
            BaseUrl = _siteSetting.BaseUrl;
        }
        public IConfigurationRoot Configuration { get; private set; }
        public virtual void ConfigureContainer(ContainerBuilder builder)
        {
            var assembly = Initializer.ProjectAssembly;
            builder.RegisterAssemblyTypes(assembly)
                .AssignableTo<IScopedDependency>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
