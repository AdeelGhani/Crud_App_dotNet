using System;

namespace Crud_App_dotNetCore.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string? Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Role { get; set; } // Admin, Manager, Cashier, Staff
        public bool EmailConfirmed { get; set; }

        public string? EmailVerificationToken { get; set; }
        public DateTime? EmailVerificationTokenExpiresAt { get; set; }

        public string? PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpiresAt { get; set; }
    }
}
