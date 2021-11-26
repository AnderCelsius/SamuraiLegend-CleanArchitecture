using AutoMapper;
using SamuraiLegend.Application.Features.Quotes.Query.GetAllQuotes;
using SamuraiLegend.Application.Features.Samurais.Commands.AddSamuraiWithQuote;
using SamuraiLegend.Application.Features.Samurais.Queries.GetAllSamurais;
using SamuraiLegend.Domain.Entities;

namespace SamuraiLegend.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            // Samurai
            CreateMap<Samurai, AddSamuraiWithQuoteCommand>().ReverseMap();
            CreateMap<Samurai, GetAllSamuraisRequest>().ReverseMap();

            // Quote
            CreateMap<Quote, GetAllQuotesRequest>()
                .ForMember(r => r.SamuraiName, q => q.MapFrom(src => src.Samurai.Name));

        }
    }
}
