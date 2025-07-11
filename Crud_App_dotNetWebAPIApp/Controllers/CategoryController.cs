using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Crud_App_dotNetApplication.DTOs.CategoryDTOs;
using Crud_App_dotNetApplication.DTOs.ProductDTOs;
using Crud_App_dotNetApplication.Interfaces.IServices;

namespace Crud_App_dotNetWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        public CategoryController(IMapper mapper, ICategoryService CategoryService)
        {
            this._mapper = mapper;
            _categoryService = CategoryService;      
        }

        [HttpPost]
        public async Task<IActionResult> PostCategory(AddCategoryDTO addCategoryDTO)
        {
            var category = await _categoryService.AddCategoryAsync(addCategoryDTO);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDTO updateCategoryDTO)
        {
            var updatedCategory = await _categoryService.UpdateCategoryAsync(id, updateCategoryDTO);
            if (updatedCategory == null) return NotFound();
            return Ok(updatedCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (!result)
                return BadRequest("Cannot delete category because it has products. Remove or reassign products first.");
            return Ok(new { message = "Category deleted successfully." });
        }

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetAllProductsByCategoryId(int id, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _categoryService.GetAllProductsByCategoryIdAsync(id, pageNumber, pageSize);
            if (result == null || result.Items == null || !result.Items.Any()) return NotFound();
            return Ok(result);
        }
    }
}
