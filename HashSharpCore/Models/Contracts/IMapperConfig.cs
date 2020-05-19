using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace HashSharpCore.Models.Contracts
{
    public interface IMapperConfig
    {
        void MapperConfig(Profile profile);
    }
}
