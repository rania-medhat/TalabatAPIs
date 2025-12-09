using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace PresistenceLayer
{
    internal static class SpecificationEvaluator 
    {
        //create query
        //_dbContext.Set<TEntity>.where(p=>p.Id==id &&).Include(p=>p.ProductBrand).Include(p=>p.ProductType)
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> inputQuery, ISpesifications<TEntity, TKey> spesifications) where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery;
            //apply criteria
            if (spesifications.Criteria is not null)
            {
                query = query.Where(spesifications.Criteria);
            }

            //apply includes
            if (spesifications.IncludeExpressions is not null && spesifications.IncludeExpressions.Count>0)
            {

                //foreach (var include in spesifications.IncludeExpressions)
                //{
                //     query.Include(include);
                //}

                //basequery+next+current
                //basequery+include(includeExpression)+include(includeExpression)...
                query=spesifications.IncludeExpressions
                    .Aggregate(query, (current, includeExpression) => current.Include(includeExpression));
            }

            return query;

        }
}
}
