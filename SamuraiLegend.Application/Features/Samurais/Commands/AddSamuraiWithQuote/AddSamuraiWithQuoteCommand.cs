using MediatR;
using SamuraiLegend.Application.Wrappers;
using SamuraiLegend.Domain.Entities;
using System.Collections.Generic;

namespace SamuraiLegend.Application.Features.Samurais.Commands.AddSamuraiWithQuote
{
    public class AddSamuraiWithQuoteCommand : IRequest<Response<Samurai>>
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string ShortStory { get; set; }
        public string Quote { get; set; }
    }
}
