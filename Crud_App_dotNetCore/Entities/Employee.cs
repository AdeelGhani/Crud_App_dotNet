using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Crud_App_dotNetCore.Entities
{
    public class Employee : BaseEntity
    {
        [Required]
        public string FName { get; set; }
        [Required]
        public string LName { get; set; }
        [Required]
        public string BasicSalary { get; set; }
        public string AllowanceSalary { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public string? Roll { get; set; }
        [ForeignKey("MarrigeID")]
        public int? MarrigeID { get; set; }
        public Marrige Marrige { get; set; }
        [ForeignKey("DesignationID")]
        public int? DesignationID { get; set; }
        public Designation Designation { get; set; }
        [ForeignKey("EmployeeTypeID")]
        public int? EmployeeTypeID { get; set; }
        public EmployeeType EmployeeType { get; set; }
    }
}
