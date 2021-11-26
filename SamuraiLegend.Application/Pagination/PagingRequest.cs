using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiLegend.Application.Pagination
{
    public class PagingRequest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
