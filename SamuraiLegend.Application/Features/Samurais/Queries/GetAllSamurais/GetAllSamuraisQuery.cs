using MediatR;
using SamuraiLegend.Application.Pagination;
using SamuraiLegend.Application.Wrappers;
using SamuraiLegend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiLegend.Application.Features.Samurais.Queries.GetAllSamurais
{
    public class GetAllSamuraisQuery : PagingRequest, IRequest<Response<PageResult<IEnumerable<GetAllSamuraisRequest>>>>
    {
    }
}
