using SamuraiLegend.Application.DTOs.Authentication;
using SamuraiLegend.Application.Wrappers;
using System.Threading.Tasks;

namespace SamuraiLegend.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Response<LoginResponse>> Login(LoginRequest model, string ipAddress);
        Task<Response<string>> Register(RegisterRequest model, string origin);
        Task<Response<string>> ResetPassword(ResetPasswordRequest model);
        Task ForgotPassword(ForgotPasswordRequest model, string origin);
        Task<Response<string>> ConfirmEmailAsync(string userId, string code);
    }
}
