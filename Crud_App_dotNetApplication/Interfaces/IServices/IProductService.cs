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
        Task<FetchProductDTO> AddProductAsync(FetchProductDTO productDto);
        Task<FetchProductDTO> UpdateProductAsync(int id, FetchProductDTO productDto);
        Task<bool> DeleteProductAsync(int id);
    }
}
