using MediatR;
using SamuraiLegend.Application.Interfaces;
using SamuraiLegend.Application.Wrappers;
using SamuraiLegend.Domain.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SamuraiLegend.Application.Features.Samurais.Queries.GetSamuraiById
{
    public class GetSamuraiByIdQuery : IRequest<Response<Samurai>>
    {
        public string Id { get; set; }

        public class GetSamuraiByIdQueryHandler : IRequestHandler<GetSamuraiByIdQuery, Response<Samurai>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger _logger;

            public GetSamuraiByIdQueryHandler(IUnitOfWork unitOfWork, ILogger logger)
            {
                _unitOfWork = unitOfWork;
                _logger = logger;
            }
            public async Task<Response<Samurai>> Handle(GetSamuraiByIdQuery request, CancellationToken cancellationToken)
            {
                var includes = new List<string>() { "Quotes", "Battles" };

                _logger.Information($"Attempting to get Samurai with Id = {request.Id}");
                var samurai = await _unitOfWork.Samurais.GetAsync(q => q.Id == request.Id, includes);
                if (samurai == null)
                {
                    _logger.Information($"Search ended with no result");
                    return Response<Samurai>.Fail($"Samurai with Id = {request.Id} does not exist");
                }

                _logger.Information($"Samurai {samurai.Name} returned");
                return Response<Samurai>.Success(string.Empty, samurai);
            }
        }
    }
}
