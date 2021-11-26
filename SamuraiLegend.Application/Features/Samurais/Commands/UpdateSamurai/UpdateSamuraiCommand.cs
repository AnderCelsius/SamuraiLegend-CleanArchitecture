using AutoMapper;
using MediatR;
using SamuraiLegend.Application.Interfaces;
using SamuraiLegend.Application.Wrappers;
using Serilog;
using System.Threading;
using System.Threading.Tasks;

namespace SamuraiLegend.Application.Features.Samurais.Commands.UpdateSamurai
{
    public class UpdateSamuraiCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ShortStory { get; set; }

        public class UpdateSamuraiCommandHandler : IRequestHandler<UpdateSamuraiCommand, Response<string>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger _logger;
            private readonly IMapper _mapper;

            public UpdateSamuraiCommandHandler(IUnitOfWork unitOfWork, ILogger logger, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(UpdateSamuraiCommand request, CancellationToken cancellationToken)
            {
                var samurai = await _unitOfWork.Samurais.GetAsync(q => q.Id == request.Id);
                if(samurai == null)
                {
                    _logger.Information($"No Samurai in record matches Id = {request.Id}");
                    return Response<string>.Fail($"No Samurai exist for Id = {request.Id}");
                }
                var result = _mapper.Map(request, samurai);
                _unitOfWork.Samurais.UpdateAsync(result);
                await _unitOfWork.Save();
                return Response<string>.Success(result.Id, $"{result.Name} was updated successfully");
            }
        }
    }
}
