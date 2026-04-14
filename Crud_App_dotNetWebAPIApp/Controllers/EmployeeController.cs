using Crud_App_dotNetApplication.DTOs.EmployeeDTOs;
using Crud_App_dotNetApplication.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Crud_App_dotNetWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeDTO employeeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var response = await employeeService.CreateAsync(employeeDTO);
            return response.Success
            ? Ok(response)
            : BadRequest(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await employeeService.GetAllEmplopeeAsync();
            return response.Success
            ? Ok(response)
            : BadRequest(response);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByID(int Id)
        {
            var response = await employeeService.GetByIDAsync(Id);
            return response.Success
            ? Ok(response)
            : BadRequest(response);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var response = await employeeService.DeleteAsync(Id);
            return response.Success
            ? Ok(response)
            : BadRequest(response);
        }
    }
}
