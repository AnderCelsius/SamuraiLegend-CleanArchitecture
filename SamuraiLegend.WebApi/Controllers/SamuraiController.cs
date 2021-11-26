using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SamuraiLegend.Application.Features.Samurais.Commands.AddSamuraiWithQuote;
using SamuraiLegend.Application.Features.Samurais.Commands.DeleteSamuraiById;
using SamuraiLegend.Application.Features.Samurais.Queries.GetAllSamurais;
using SamuraiLegend.Application.Features.Samurais.Queries.GetSamuraiById;
using System.Threading.Tasks;

namespace SamuraiLegend.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SamuraiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        
        
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllSamuraisQuery filter)
        {
            var result = await Mediator.Send(new GetAllSamuraisQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber });
            return StatusCode(result.StatusCode, result);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await Mediator.Send(new GetSamuraiByIdQuery { Id = id });
            return StatusCode(result.StatusCode, result);
        }

        //POST api/<controller>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddSamurai(AddSamuraiWithQuoteCommand command)
        {
            var result = await Mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await Mediator.Send(new DeleteSamuraiByIdCommand { Id = id });
            return StatusCode(result.StatusCode, result);
        }
    }
}
