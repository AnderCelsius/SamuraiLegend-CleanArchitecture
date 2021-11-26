using Microsoft.EntityFrameworkCore;
using SamuraiLegend.Application.Interfaces.Repositories;
using SamuraiLegend.Domain.Entities;
using SamuraiLegend.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiLegend.Infrastructure.Persistence.Repositories
{
    public class QuoteRepository : GenericRepository<Quote>, IQuoteRepository
    {
        private readonly DbSet<Quote> _quotes;
        public QuoteRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _quotes = dbContext.Set<Quote>();
        }

        public IQueryable<Quote> GetAll(Expression<Func<Quote, bool>> expression = null, Func<IQueryable<Quote>, IOrderedQueryable<Quote>> orderBy = null, List<string> includes = null)
        {
            IQueryable<Quote> query = _quotes;
            if (expression != null)
            {
                query = _quotes.Where(expression);
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

            return query;
        }

    }
}
