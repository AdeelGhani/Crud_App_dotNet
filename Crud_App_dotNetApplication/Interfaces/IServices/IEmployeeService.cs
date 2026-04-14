using Crud_App_dotNetApplication.DTOs.EmployeeDTOs;
using Crud_App_dotNetApplication.Wrappers;

namespace Crud_App_dotNetApplication.Interfaces.IServices
{
    public interface IEmployeeService
    {
        Task<ApiResponse<List<EmployeeDTO>>> GetAllEmplopeeAsync();
        Task<ApiResponse<EmployeeDTO>> GetByIDAsync(int Id);
        Task<ApiResponse<EmployeeDTO>> UpdateAsync(int Id, EmployeeDTO  employeeDTO);
        Task<ApiResponse<EmployeeDTO>> CreateAsync(EmployeeDTO  employeeDTO);
        Task<ApiResponse<EmployeeDTO>> DeleteAsync(int Id);
    }
}
