using SamuraiLegend.Application.Interfaces;
using SamuraiLegend.Application.Interfaces.Repositories;
using SamuraiLegend.Domain.Entities;
using SamuraiLegend.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiLegend.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private ISamuraiRepository _samurais;
        private IQuoteRepository _quotes;
        private IGenericRepository<Battle> _battles;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns a new instance of GenericRepositoy<Samurai> if _samurais is null
        /// </summary>
        public ISamuraiRepository Samurais => _samurais ??= new SamuraiRepository(_context);

        public IQuoteRepository Quotes => _quotes ??= new QuoteRepository(_context);

        public IGenericRepository<Battle> Battles => _battles ??= new GenericRepository<Battle>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    
    }
}
