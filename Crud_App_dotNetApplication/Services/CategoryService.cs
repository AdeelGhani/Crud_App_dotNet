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

        public CategoryService(IMapper mapper ,IUnitOfWork unitOfWork)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }
        public async Task<FetchCategoryDTO> AddCategoryAsync(AddCategoryDTO CategoryDto)
        {
            var category = _mapper.Map<Category>(CategoryDto);
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<FetchCategoryDTO>(category);
        }

        public async Task<IEnumerable<FetchCategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            return _mapper.Map<IEnumerable<FetchCategoryDTO>>(categories);
        }

        public async Task<FetchCategoryDTO> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            return category == null ? null : _mapper.Map<FetchCategoryDTO>(category);
        }

        public async Task<FetchCategoryDTO> UpdateCategoryAsync(int id, UpdateCategoryDTO updateCategoryDTO)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null) return null;
            _mapper.Map(updateCategoryDTO, category);
            _unitOfWork.Categories.Update(category);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<FetchCategoryDTO>(category);
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
    }
}
