using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using HashSharpCore.Models.Contracts;

namespace HashSharpCore.Models
{
    public class CustomMappingProfile : Profile
    {
        public CustomMappingProfile(IEnumerable<IMapperConfig> mapperConfs)
        {
            foreach (var mapperConf in mapperConfs)
                mapperConf.MapperConfig(this);
        }
    }
}
