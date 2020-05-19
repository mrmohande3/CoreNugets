using System;
using System.Collections.Generic;
using System.Text;
using HashSharpCore.DataLayer;
using HashSharpCore.DataLayer.Models;
using HashSharpCore.Models.Contracts;
using HashSharpCore.Services.Contracts;

namespace HashSharpCore.Services
{
    public class RepositoryWrapper : IRepositoryWrapper, IScopedDependency
    {
        private readonly ApplicationContext _context;
        public RepositoryWrapper(ApplicationContext context)
        {
            _context = context;
        }
        public IBaseRepository<T> SetRepository<T>() where T : ApiEntity
        {
            IBaseRepository<T> repository = new BaseRepository<T>(_context);
            return repository;
        }
    }
}
}
