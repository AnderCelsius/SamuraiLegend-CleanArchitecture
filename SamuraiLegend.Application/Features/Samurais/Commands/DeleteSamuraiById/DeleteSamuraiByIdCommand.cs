using MediatR;
using SamuraiLegend.Application.Interfaces;
using SamuraiLegend.Application.Wrappers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SamuraiLegend.Application.Features.Samurais.Commands.DeleteSamuraiById
{
    public class DeleteSamuraiByIdCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }

        public class DeleteSamuraiByIdCommandHandler : IRequestHandler<DeleteSamuraiByIdCommand, Response<string>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger _logger;

            public DeleteSamuraiByIdCommandHandler(IUnitOfWork unitOfWork, ILogger logger)
            {
                _unitOfWork = unitOfWork;
                _logger = logger;
            }
            public async Task<Response<string>> Handle(DeleteSamuraiByIdCommand request, CancellationToken cancellationToken)
            {
                var samurai = await _unitOfWork.Samurais.GetAsync(q => q.Id == request.Id);
                if (samurai == null)
                {
                    _logger.Information($"Operation failed because no Samurai belonging to Id = {request.Id} was found in record");
                    return Response<string>.Fail($"Samurai with Id = {request.Id} does not exist in records");
                }
                _unitOfWork.Samurais.Delete(samurai);
                await _unitOfWork.Save();
                _logger.Information($"{samurai.Name} was successfully deleted from records");
                return Response<string>.Success(request.Id, $"{samurai.Name} deleted successfully");
            }
        }
    }
}
