using System.ComponentModel.DataAnnotations;

namespace Crud_App_dotNetCore.Entities
{
    public class EmployeeType:BaseEntity
    {
        [Required]
        public string Type { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
