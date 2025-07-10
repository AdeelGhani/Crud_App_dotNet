using System.ComponentModel.DataAnnotations;

namespace Crud_App_dotNetCore.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(50)]
        public string Role { get; set; } // e.g., Admin, Customer
    }
} 