using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiLegend.Application.Features.Quotes.Query.GetAllQuotes
{
    public class GetAllQuotesRequest
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string SamuraiName { get; set; }
    }
}
