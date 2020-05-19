using System;
using System.Collections.Generic;
using System.Text;
using HashSharpCore.DataLayer.Models;

namespace HashSharpCore.Services.Contracts
{
    public interface IRepositoryWrapper
    {
        IBaseRepository<T> SetRepository<T>() where T : ApiEntity;
    }
}
