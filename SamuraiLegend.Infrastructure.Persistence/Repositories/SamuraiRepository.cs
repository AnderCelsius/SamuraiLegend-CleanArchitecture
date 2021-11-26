using Microsoft.EntityFrameworkCore;
using SamuraiLegend.Application.Interfaces.Repositories;
using SamuraiLegend.Domain.Entities;
using SamuraiLegend.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SamuraiLegend.Infrastructure.Persistence.Repositories
{
    public class SamuraiRepository : GenericRepository<Samurai>, ISamuraiRepository
    {
        private readonly DbSet<Samurai> _samurai;
        public SamuraiRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _samurai = dbContext.Set<Samurai>();
        }

        public IQueryable<Samurai> GetAll(Expression<Func<Samurai, bool>> expression = null, Func<IQueryable<Samurai>, IOrderedQueryable<Samurai>> orderBy = null, List<string> includes = null)
        {
            IQueryable<Samurai> query = _samurai;
            if (expression != null)
            {
                query = _samurai.Where(expression);
            }

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return  query;
        }

        public Task<bool> IsUniqueSamuraiNameAsync(string name)
        {
            return _samurai.AllAsync(s => s.Name != name);
        }
    }
}
