using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Crud_App_dotNetApplication.DTOs.ProductDTOs;
using Crud_App_dotNetApplication.Interfaces;
using Crud_App_dotNetApplication.Interfaces.IRepository;
using Crud_App_dotNetApplication.Interfaces.IServices;
using Crud_App_dotNetApplication.Repositories;
using Crud_App_dotNetCore.Entities;

namespace Crud_App_dotNetApplication.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<FetchProductDTO> AddProductAsync(AddProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            product.UpdatedDate = DateTime.Now;
            product.CreatedDate = DateTime.Now;
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<FetchProductDTO>(product);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null) return false;

            _unitOfWork.Products.Remove(product);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<PaginatedResult<FetchProductDTO>> GetAllProductsAsync(int pageNumber, int pageSize)
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            if (products == null)
            {
                return null;
            }
            var totalCount = products.Count();
            var pagedProducts = products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return new PaginatedResult<FetchProductDTO>
            {
                Items = _mapper.Map<List<FetchProductDTO>>(pagedProducts),
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<FetchProductDTO> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            return _mapper.Map<FetchProductDTO>(product);
        }

        public async Task<FetchProductDTO> UpdateProductAsync(int id, UpdateProductDTO productDto)
        {
            var existingProduct = await _unitOfWork.Products.GetByIdAsync(id);
            if (existingProduct == null) return null;

            _mapper.Map(productDto, existingProduct);
            _unitOfWork.Products.Update(existingProduct);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<FetchProductDTO>(existingProduct);
        }
    }
}
