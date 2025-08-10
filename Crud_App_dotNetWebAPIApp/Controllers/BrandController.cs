using Crud_App_dotNetApplication.DTOs.BrandDTOs;
using Crud_App_dotNetApplication.Interfaces.IServices;
using Crud_App_dotNetCore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crud_App_dotNetWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            this._brandService = brandService;
        }
        [HttpGet]
        public async Task<IActionResult> FetchAllBrands()
        {
            var brand = await _brandService.GetAllBrands();
            if (brand == null) return BadRequest(new { message = "Failed to fetch brands!!!." });
            return Ok(brand);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdBrand(int Id)
        {
            var brand = await _brandService.GetByIdBrands(Id);
            if (brand == null) return BadRequest($"Unable to load brand");
            return Ok(brand);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBrand(AddBrandDTO model)
        {
            var brand = await _brandService.CreateBrand(model);
            if (brand == null) return BadRequest($"Unable to load brand");
            return Ok(brand);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateBrand(UpdateBrandDTO model, int Id)
        {
            var brand = await _brandService.UpdateBrand(model, Id);
            if (brand == null) return BadRequest($"Unable to load brand");
            return Ok(brand);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteBrand(int Id)
        {
            var brand = await _brandService.DeleteBrand(Id);
            if (!brand) return BadRequest(new { message = "Cannot delete category because it has products. Remove or reassign products first." });
            return Ok(new { message = "Category deleted successfully."});
        }
    }
}
