using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SamuraiLegend.Application.DTOs.Authentication;
using SamuraiLegend.Application.Interfaces;
using SamuraiLegend.Application.Wrappers;
using System.Threading.Tasks;

namespace SamuraiLegend.WebApi.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authService;

        public AccountController(IAuthenticationService authService)
        {
            _authService = authService;
        }


        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<LoginResponse>>> Login([FromBody]LoginRequest request)
        {
            var result = await _authService.Login(request, GenerateIPAddress());
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<LoginResponse>>> Register([FromBody] RegisterRequest model)
        {
            var origin = Request.Headers["origin"];
            var result = await _authService.Register(model, origin);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("confirm-email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<string>>> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string code)
        {
            var result = await _authService.ConfirmEmailAsync(userId, code);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [Route("forgot-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<string>>> ForgotPassword(ForgotPasswordRequest model)
        {
            await _authService.ForgotPassword(model, Request.Headers["origin"]);
            return Ok();
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {
            return Ok(await _authService.ResetPassword(model));
        }

        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
