using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoMapper;
using HashSharpCore.Models;
using HashSharpCore.Models.Contracts;

namespace HashSharpCore.Extensions
{ 
    public static class AutoMapperConfigExtensions
    {
        public static void ConfigurationMapper()
        {
            Mapper.Initialize(config => { config.AddCustomMapping(Assembly.GetEntryAssembly()); });
            Mapper.Configuration.CompileMappings();
        }

        public static void AddCustomMapping(this IMapperConfigurationExpression expression, Assembly assemblies)
        {
            var allTypes = assemblies.ExportedTypes;
            var list = allTypes
                .Where(type => type.IsClass && !type.IsAbstract && type.GetInterfaces().Contains(typeof(IMapperConfig)))
                .Select(type => (IMapperConfig)Activator.CreateInstance(type)).ToList();
            expression.AddProfile(new CustomMappingProfile(list));
        }
    }
}
