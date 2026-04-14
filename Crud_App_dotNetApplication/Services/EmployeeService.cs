using AutoMapper;
using Crud_App_dotNetApplication.DTOs.EmployeeDTOs;
using Crud_App_dotNetApplication.Interfaces;
using Crud_App_dotNetApplication.Interfaces.IServices;
using Crud_App_dotNetApplication.Wrappers;
using Crud_App_dotNetCore.Entities;
using Crud_App_dotNetCore.Interfaces.IServices;
using System.Web.Mvc;
//using System.Web.Http.ModelBinding;

namespace Crud_App_dotNetApplication.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ITenantProvider tenant;

        public EmployeeService(IUnitOfWork _unitOfWork, IMapper mapper, ITenantProvider tenant)
        {
            unitOfWork = _unitOfWork;
            this.mapper = mapper;
            this.tenant = tenant;
        }
        public async Task<ApiResponse<EmployeeDTO>> CreateAsync(EmployeeDTO employeeDTO)
        {
            ApiResponse<EmployeeDTO> response = new ApiResponse<EmployeeDTO>();

            var map = mapper.Map<Employee>(employeeDTO);

            await unitOfWork.Employee.AddAsync(map);
            await unitOfWork.SaveChangesAsync();

            var result = mapper.Map<EmployeeDTO>(map);

            response.Data = result;
            response.Success = true;
            response.Errors = null;
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }

        public async Task<ApiResponse<EmployeeDTO>> DeleteAsync(int Id)
        {
            var employee = await unitOfWork.Employee.GetByIdAsync(Id);
            if(employee == null)
            {
                return ApiResponse<EmployeeDTO>.FailureResponse(new List<string> { "Employee Not Found!" }, "Not Found!!");
            }
            unitOfWork.Employee.Remove(employee);
            await unitOfWork.SaveChangesAsync();

            var result = mapper.Map<EmployeeDTO>(employee);
            return ApiResponse<EmployeeDTO>.SuccessResponse(result, "Fetched!");

        }

        public async Task<ApiResponse<List<EmployeeDTO>>> GetAllEmplopeeAsync()
        {
            var qwe = tenant.GetTenantId();
            ApiResponse<List<Employee>> response = new ApiResponse<List<Employee>>();
            var exiting = await unitOfWork.Employee.IgnoreQueryFilters();
            if (exiting == null)
            {
                ApiResponse<EmployeeDTO>.FailureResponse(new List<string> { "Model Can't be Empty!!" }, "Validation Error");
            }
            var map = mapper.Map<List<EmployeeDTO>>(exiting);

            return ApiResponse<List<EmployeeDTO>>.SuccessResponse(map, "Data Fetched Successfully!!!");
        }

        public async Task<ApiResponse<EmployeeDTO>> GetByIDAsync(int Id)
        {
            var employee = await unitOfWork.Employee.GetByIdAsync(Id);

            if(employee == null)
            {
                ApiResponse<EmployeeDTO>.FailureResponse(new List<string> { "Record Not Found!!" }, "Validation Error");
            }

            var map = mapper.Map<EmployeeDTO>(employee);

            return ApiResponse<EmployeeDTO>.SuccessResponse(map, "record fetched Successull!!!");
        }

        public async Task<ApiResponse<EmployeeDTO>> UpdateAsync(int Id, EmployeeDTO employeeDTO)
        {
            ApiResponse<EmployeeDTO> response = new ApiResponse<EmployeeDTO>();
            if(employeeDTO == null)
            {
                return ApiResponse<EmployeeDTO>.FailureResponse(new List<string> { "Validation Error" }, "Validation Error");
            }
            var exsting = await unitOfWork.Employee.GetByIdAsync(Id);
            if(exsting == null)
            {
                return ApiResponse<EmployeeDTO>.FailureResponse(new List<string> { "Validation Error" }, "Validation Error");
            }
            
            unitOfWork.Employee.Update(exsting);
            await unitOfWork.SaveChangesAsync();

             var result = mapper.Map<EmployeeDTO>(exsting);
            return ApiResponse<EmployeeDTO>.SuccessResponse(result, "Success!"); 
        }
    }
}
