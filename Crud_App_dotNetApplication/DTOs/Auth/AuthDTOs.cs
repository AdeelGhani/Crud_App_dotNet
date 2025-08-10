namespace Crud_App_dotNetApplication.DTOs.Auth
{
    public class RegisterDTO
    {
        public string Username { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class LoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class AuthResponseDTO
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public bool EmailConfirmed { get; set; }
    }

    public class ResetPasswordDTO
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
