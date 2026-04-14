using Crud_App_dotNetCore.Entities;
namespace Crud_App_dotNetApplication.DTOs.EmployeeDTOs
{
    public class EmployeeDTO
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string BasicSalary { get; set; }
        public string AllowanceSalary { get; set; }
        public string Description { get; set; }
        public string? Roll { get; set; }
        public int? MarrigeID { get; set; }
        public int? DesignationID { get; set; }
        public int? EmployeeTypeID { get; set; }
    }
}
