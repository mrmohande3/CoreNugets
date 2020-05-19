using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HashSharpCore.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace HashSharpCore.Services.Contracts
{

    public interface IBaseRepository<T> where T : class, IApiEntity
    {
        DbSet<T> Entities { get; }
        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }

        void Add(T entity, bool saveNow = true);
        Task AddAsync(T entity, CancellationToken cancellationToken, bool saveNow = true);
        void AddRange(IEnumerable<T> entities, bool saveNow = true);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken, bool saveNow = true);
        void Attach(T entity);
        void Delete(T entity, bool saveNow = true);
        Task DeleteAsync(T entity, CancellationToken cancellationToken, bool saveNow = true);
        void DeleteRange(IEnumerable<T> entities, bool saveNow = true);
        Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken, bool saveNow = true);
        void Detach(T entity);
        T GetById(params object[] ids);
        ValueTask<T> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);
        void LoadCollection<TProperty>(T entity, Expression<Func<T, IEnumerable<TProperty>>> collectionProperty) where TProperty : class;
        Task LoadCollectionAsync<TProperty>(T entity, Expression<Func<T, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken) where TProperty : class;
        void LoadReference<TProperty>(T entity, Expression<Func<T, TProperty>> referenceProperty) where TProperty : class;
        Task LoadReferenceAsync<TProperty>(T entity, Expression<Func<T, TProperty>> referenceProperty, CancellationToken cancellationToken) where TProperty : class;
        void Update(T entity, bool saveNow = true);
        Task UpdateAsync(T entity, CancellationToken cancellationToken, bool saveNow = true);
        void UpdateRange(IEnumerable<T> entities, bool saveNow = true);
        Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken, bool saveNow = true);
    }
}
