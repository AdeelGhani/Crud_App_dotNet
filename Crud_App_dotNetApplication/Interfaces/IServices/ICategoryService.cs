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
        Task<CategoryDTO> AddCategoryAsync(CategoryDTO CategoryDto);
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO> GetCategoryByIdAsync(int id);
        Task<CategoryDTO> UpdateCategoryAsync(int id, CategoryDTO updateCategoryDTO);
        Task<bool> DeleteCategoryAsync(int id);
        Task<PaginatedResult<FetchProductDTO>> GetAllProductsByCategoryIdAsync(int categoryId, int pageNumber, int pageSize);
    }
}
  