using AutoMapper;
using MediatR;
using SamuraiLegend.Application.Features.Samurais.Commands.AddSamuraiWithQuote;
using SamuraiLegend.Application.Interfaces;
using SamuraiLegend.Application.Wrappers;
using SamuraiLegend.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SamuraiLegend.Application.Features.Samurais.Commands.CreateSamuraiWithQuote
{
    public class AddSamuraiWithQuoteCommandHandler : IRequestHandler<AddSamuraiWithQuoteCommand, Response<Samurai>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AddSamuraiWithQuoteCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<Samurai>> Handle(AddSamuraiWithQuoteCommand request, CancellationToken cancellationToken)
        {
            var samurai = _mapper.Map<Samurai>(request);
            var quote = new Quote()
            {
                Text = request.Quote
            };
            samurai.Quotes.Add(quote);
            await _unitOfWork.Samurais.AddAsync(samurai);
            await _unitOfWork.Save();
            return Response<Samurai>.Success($"{samurai.Name} added succesfully", samurai);
        }
    }
}
