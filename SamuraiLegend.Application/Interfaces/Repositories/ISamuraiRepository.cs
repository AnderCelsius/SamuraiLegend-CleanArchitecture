using SamuraiLegend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiLegend.Application.Interfaces.Repositories
{
    public interface ISamuraiRepository : IGenericRepository<Samurai>
    {
        Task<bool> IsUniqueSamuraiNameAsync(string name);
        IQueryable<Samurai> GetAll(
            Expression<Func<Samurai, bool>> expression = null,
            Func<IQueryable<Samurai>, IOrderedQueryable<Samurai>> orderBy = null,
            List<string> includes = null
        );

    }
}
