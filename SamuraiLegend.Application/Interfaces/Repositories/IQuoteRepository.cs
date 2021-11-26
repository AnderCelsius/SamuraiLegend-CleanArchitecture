using SamuraiLegend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiLegend.Application.Interfaces.Repositories
{
    public interface IQuoteRepository : IGenericRepository<Quote> 
    {
        IQueryable<Quote> GetAll(Expression<Func<Quote, bool>> expression = null, Func<IQueryable<Quote>, IOrderedQueryable<Quote>> orderBy = null, List<string> includes = null);

    }
}
