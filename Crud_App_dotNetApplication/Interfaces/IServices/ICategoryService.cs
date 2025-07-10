using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud_App_dotNetApplication.DTOs.CategoryDTOs;
using Crud_App_dotNetApplication.DTOs.ProductDTOs;

namespace Crud_App_dotNetApplication.Interfaces.IServices
{
    public interface ICategoryService
    {
        Task<FetchCategoryDTO> AddCategoryAsync(AddCategoryDTO CategoryDto);
        Task<IEnumerable<FetchCategoryDTO>> GetAllCategoriesAsync();
        Task<FetchCategoryDTO> GetCategoryByIdAsync(int id);
        Task<FetchCategoryDTO> UpdateCategoryAsync(int id, UpdateCategoryDTO updateCategoryDTO);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
  