using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using PresistenceLayer.Data;

namespace PresistenceLayer.Repositories
{
    public class UnitOfWork(StoreDBContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name; //product

            //if (_repositories.ContainsKey(typeName)) //لو الريبو فيه ال key

            //    return (IGenericRepository<TEntity, TKey>)_repositories[typeName];

            if (_repositories.TryGetValue(typeName, out object? value)) //لو الريبو فيه ال key

                return (IGenericRepository<TEntity, TKey>)value;
            else
            {
                //create new repository object
                var repo = new GenericRepository<TEntity, TKey>(_dbContext);
                //add it to dictionary
                _repositories.Add(typeName, repo);
                return repo;
            }

        }

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();

    }
}
