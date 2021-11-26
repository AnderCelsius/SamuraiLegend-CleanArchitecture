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

namespace SamuraiLegend.Application.Features.Quotes.Query.GetAllQuotes
{
    public class GetAllQuotesQuery : PagingRequest, IRequest<Response<PageResult<IEnumerable<GetAllQuotesRequest>>>>
    {

    }

    public class GetAllQuotesQueryHandler : IRequestHandler<GetAllQuotesQuery, Response<PageResult<IEnumerable<GetAllQuotesRequest>>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetAllQuotesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Response<PageResult<IEnumerable<GetAllQuotesRequest>>>> Handle(GetAllQuotesQuery request, CancellationToken cancellationToken)
        {
            _logger.Information("Getting all quotes");
            var quotesQuery = _unitOfWork.Quotes.GetAll(includes: new List<string> { "Samurai" });
            var quoteList = await quotesQuery.PaginationAsync<Quote, GetAllQuotesRequest>(request.PageSize, request.PageNumber, _mapper);
            return new Response<PageResult<IEnumerable<GetAllQuotesRequest>>>(StatusCodes.Status200OK, true, "List of all quotes", quoteList);
        }
    }
}
