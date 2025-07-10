using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Crud_App_dotNetApplication.DTOs.CategoryDTOs;
using Crud_App_dotNetApplication.DTOs.ProductDTOs;
using Crud_App_dotNetApplication.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;

namespace Crud_App_dotNetWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> PostCategory(AddCategoryDTO addCategoryDTO)
        {
            var category = await _categoryService.AddCategoryAsync(addCategoryDTO);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDTO updateCategoryDTO)
        {
            var updatedCategory = await _categoryService.UpdateCategoryAsync(id, updateCategoryDTO);
            if (updatedCategory == null) return NotFound();
            return Ok(updatedCategory);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
