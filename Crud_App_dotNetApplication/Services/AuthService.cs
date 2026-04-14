using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Crud_App_dotNetApplication.DTOs.Auth;
using Crud_App_dotNetApplication.Interfaces;
using Crud_App_dotNetApplication.Interfaces.IServices;
using Crud_App_dotNetCore.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Crud_App_dotNetApplication.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            var existing = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Username == registerDTO.Username);
            if (existing != null) return null;

            CreatePasswordHash(registerDTO.Password, out string hash, out string salt);

            var user = new User
            {
                Username = registerDTO.Username,
                Email = registerDTO.Email,
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = registerDTO.Role,
                EmailConfirmed = false,
                CreatedAt = DateTime.UtcNow
            };
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            // optional: send email verification token
            return await GenerateAuthResponse(user);
        }

        public async Task<AuthResponseDTO> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Username == loginDTO.Username);
            if (user == null) return null;
            if (!VerifyPassword(loginDTO.Password, user.PasswordHash, user.PasswordSalt)) return null;

            return await GenerateAuthResponse(user);
        }

        public Task LogoutAsync(string token)
        {
            // With stateless JWT, logout is a client-side operation or token blacklist; skipping for brevity
            return Task.CompletedTask;
        }

        public async Task<bool> SendEmailVerificationAsync(string usernameOrEmail)
        {
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail);
            if (user == null) return false;
            user.EmailVerificationToken = Guid.NewGuid().ToString("N");
            user.EmailVerificationTokenExpiresAt = DateTime.UtcNow.AddHours(2);
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();
            // TODO: send email using provider
            return true;
        }

        public async Task<bool> VerifyEmailAsync(string token)
        {
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.EmailVerificationToken == token);
            if (user == null) return false;
            if (user.EmailVerificationTokenExpiresAt < DateTime.UtcNow) return false;
            user.EmailConfirmed = true;
            user.EmailVerificationToken = null;
            user.EmailVerificationTokenExpiresAt = null;
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SendPasswordResetAsync(string usernameOrEmail)
        {
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail);
            if (user == null) return false;
            user.PasswordResetToken = Guid.NewGuid().ToString("N");
            user.PasswordResetTokenExpiresAt = DateTime.UtcNow.AddHours(1);
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();
            // TODO: send email containing token
            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO)
        {
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == resetPasswordDTO.Token);
            if (user == null) return false;
            if (user.PasswordResetTokenExpiresAt < DateTime.UtcNow) return false;

            CreatePasswordHash(resetPasswordDTO.NewPassword, out string hash, out string salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpiresAt = null;
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        private async Task<AuthResponseDTO> GenerateAuthResponse(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("appUserID", user.Id.ToString())
            };

            var jwtKey = _configuration["Jwt:Key"] ?? "dev_secret_key_change_me";
            var jwtIssuer = _configuration["Jwt:Issuer"] ?? "dev-issuer";
            var jwtAudience = _configuration["Jwt:Audience"] ?? "dev-audience";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds 
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthResponseDTO
            {
                Token = tokenString,
                Username = user.Username,
                Role = user.Role,
                EmailConfirmed = user.EmailConfirmed,
                AppUserId = user.Id
            };
        }

        private static void CreatePasswordHash(string password, out string hash, out string salt)
        {
            // PBKDF2 with SHA256, 100k iterations, 32-byte key
            const int iterations = 100_000;
            const int saltSize = 16;
            const int keySize = 32;

            var saltBytes = RandomNumberGenerator.GetBytes(saltSize);
            var key = Rfc2898DeriveBytes.Pbkdf2(
                password,
                saltBytes,
                iterations,
                HashAlgorithmName.SHA256,
                keySize);

            hash = Convert.ToBase64String(key);
            salt = Convert.ToBase64String(saltBytes);
        }

        private static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            const int iterations = 100_000;
            const int keySize = 32;
            var saltBytes = Convert.FromBase64String(storedSalt);
            var expectedHash = Convert.FromBase64String(storedHash);
            var actualHash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                saltBytes,
                iterations,
                HashAlgorithmName.SHA256,
                keySize);
            return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
        }
    }
}
