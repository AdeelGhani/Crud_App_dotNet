using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud_App_dotNetApplication.DTOs.ProductDTOs;
using Crud_App_dotNetCore.Entities;

namespace Crud_App_dotNetApplication.Interfaces.IServices
{
    public interface IProductService
    {
        Task<FetchProductDTO> GetProductByIdAsync(int id);
        Task<PaginatedResult<FetchProductDTO>> GetAllProductsAsync(int pageNumber, int pageSize);
        Task<FetchProductDTO> AddProductAsync(AddProductDTO productDto);
        Task<FetchProductDTO> UpdateProductAsync(int id, UpdateProductDTO productDto);
        Task<bool> DeleteProductAsync(int id);
    }
}
