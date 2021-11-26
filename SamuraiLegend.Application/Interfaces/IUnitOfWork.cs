using SamuraiLegend.Application.Interfaces.Repositories;
using SamuraiLegend.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace SamuraiLegend.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ISamuraiRepository Samurais { get; }
        IQuoteRepository Quotes { get; }
        IGenericRepository<Battle> Battles { get; }
        Task Save();
    }
}
