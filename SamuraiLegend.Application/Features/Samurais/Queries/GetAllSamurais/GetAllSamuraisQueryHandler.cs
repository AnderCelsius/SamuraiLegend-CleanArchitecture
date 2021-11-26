using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SamuraiLegend.Application.Interfaces;
using SamuraiLegend.Application.Pagination;
using SamuraiLegend.Application.Wrappers;
using SamuraiLegend.Domain.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SamuraiLegend.Application.Features.Samurais.Queries.GetAllSamurais
{
    public class GetAllSamuraisQueryHandler : IRequestHandler<GetAllSamuraisQuery, Response<PageResult<IEnumerable<GetAllSamuraisRequest>>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetAllSamuraisQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Response<PageResult<IEnumerable<GetAllSamuraisRequest>>>> Handle(GetAllSamuraisQuery request, CancellationToken cancellationToken)
        {
            _logger.Information("Getting all samurais");
            var samuraisQuery = _unitOfWork.Samurais.GetAll(includes: new List<string> { "Quotes" });
            var samuraiList = await samuraisQuery.PaginationAsync<Samurai, GetAllSamuraisRequest>(request.PageSize, request.PageNumber, _mapper);
            return new Response<PageResult<IEnumerable<GetAllSamuraisRequest>>>(StatusCodes.Status200OK, true, "List of all samurais", samuraiList);
        }
    }
}
