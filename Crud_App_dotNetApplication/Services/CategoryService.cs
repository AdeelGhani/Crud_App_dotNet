using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Crud_App_dotNetApplication.DTOs.CategoryDTOs;
using Crud_App_dotNetApplication.DTOs.ProductDTOs;
using Crud_App_dotNetApplication.Interfaces;
using Crud_App_dotNetApplication.Interfaces.IServices;
using Crud_App_dotNetCore.Entities;

namespace Crud_App_dotNetApplication.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }
        public async Task<CategoryDTO> AddCategoryAsync(CategoryDTO CategoryDto)
        {
            var category = _mapper.Map<Category>(CategoryDto);

            category.CreatedDate = DateTime.Now;
            category.UpdatedDate = DateTime.Now;

            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null)
            {
                throw new Exception("Category Not Found!!!");
            }
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<CategoryDTO> UpdateCategoryAsync(int id, CategoryDTO updateCategoryDTO)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null)
            {
                throw new Exception("Category Not Found!!!");
            }
            category.CategoryName = updateCategoryDTO.CategoryName;
            category.CategoryDescription = updateCategoryDTO.CategoryDescription ?? "";
            category.IsActive = updateCategoryDTO.IsActive;
            category.UpdatedDate = DateTime.Now;
            //_mapper.Map(updateCategoryDTO, category);
            _unitOfWork.Categories.Update(category);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null) return false;
            _unitOfWork.Categories.Remove(category);
            try
            {
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                // Check for foreign key constraint violation (SQL error 547)
                if (ex.InnerException != null && ex.InnerException.Message.Contains("REFERENCE constraint"))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<PaginatedResult<FetchProductDTO>> GetAllProductsByCategoryIdAsync(int categoryId, int pageNumber, int pageSize)
        {
            var products = await _unitOfWork.Products.Where(p => p.CategoryId == categoryId);
            var totalCount = products?.Count() ?? 0;
            var pagedProducts = (products ?? new List<Product>())
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
    }
}
