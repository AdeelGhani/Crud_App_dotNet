﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Crud_App_dotNetApplication.DTOs.ProductDTOs;
using Crud_App_dotNetApplication.Interfaces.IServices;

namespace Crud_App_dotNetWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductController(IMapper mapper,IProductService productService)
        {
            this._mapper = mapper;
            this._productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid product ID." });
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound(new { message = "Product not found." });
            return Ok(product);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _productService.GetAllProductsAsync(pageNumber, pageSize);
            if (result == null || result.Items == null || !result.Items.Any())
                return NotFound(new { message = "No products found." });
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> PostProduct(AddProductDTO addProductDTO)
        {
            if (addProductDTO == null)
                return BadRequest(new { message = "Product data is required." });
            var product = await _productService.AddProductAsync(addProductDTO);
            if (product == null) return BadRequest(new { message = "Failed to create product." });
            return Ok(product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> GetProduct(UpdateProductDTO updateProductDTO , int id)
        {
            if (updateProductDTO == null)
                return BadRequest(new { message = "Update data is required." });
            var product = await _productService.UpdateProductAsync(id,updateProductDTO);
            if (product == null) return NotFound(new { message = "Product not found or update failed." });
            return Ok(product);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result) return NotFound(new { message = "Product not found." });
            return Ok(new { message = "Product deleted successfully." });
        }
    }
}
