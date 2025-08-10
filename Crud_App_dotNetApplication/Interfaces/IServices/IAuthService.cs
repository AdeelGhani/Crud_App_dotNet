using System.Threading.Tasks;
using Crud_App_dotNetApplication.DTOs.Auth;

namespace Crud_App_dotNetApplication.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDTO);
        Task<AuthResponseDTO> LoginAsync(LoginDTO loginDTO);
        Task LogoutAsync(string token);

        Task<bool> SendEmailVerificationAsync(string usernameOrEmail);
        Task<bool> VerifyEmailAsync(string token);

        Task<bool> SendPasswordResetAsync(string usernameOrEmail);
        Task<bool> ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO);
    }
}
