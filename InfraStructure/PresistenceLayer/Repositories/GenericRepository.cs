using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using PresistenceLayer.Data;

namespace PresistenceLayer.Repositories
{
    public class GenericRepository<TEntity, TKey> (StoreDBContext _dbContext)
        : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task AddAsync(TEntity entity) 
            => await _dbContext.Set<TEntity>().AddAsync(entity);


        public void Delete(TEntity entity)
        =>  _dbContext.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync() 
            => await _dbContext.Set<TEntity>().ToListAsync();


        public async Task<TEntity> GetByIdAsync(TKey id)
        => await _dbContext.Set<TEntity>().FindAsync(id);

        public void Update(TEntity entity)
        =>  _dbContext.Set<TEntity>().Update(entity);

        #region with specifications

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpesifications<TEntity, TKey> spesifications)
        {
           //var baseQuery= _dbContext.Set<TEntity>();

           // if (spesifications.Criteria is not null)
           // {
           //     var criteria = spesifications.Criteria;
           //     baseQuery = baseQuery.Where(criteria);
           // }

            return await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), spesifications)
                .ToListAsync();
        }
        public async Task<TEntity> GetByIdAsync(ISpesifications<TEntity, TKey> spesifications)
        {
            //var baseQuery = _dbContext.Set<TEntity>();

            //if (spesifications.Criteria is not null)
            //{
            //    var criteria = spesifications.Criteria;
            //    baseQuery = baseQuery.Where(criteria);
            //}
            return await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), spesifications)
                .FirstOrDefaultAsync();

        }
        #endregion
    }
}
