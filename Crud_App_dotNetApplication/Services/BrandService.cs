using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Crud_App_dotNetApplication.DTOs.BrandDTOs;
using Crud_App_dotNetApplication.Interfaces;
using Crud_App_dotNetApplication.Interfaces.IServices;
using Crud_App_dotNetCore.Entities;

namespace Crud_App_dotNetApplication.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BrandService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<AddBrandDTO> CreateBrand(AddBrandDTO model)
        {
            var brand = _mapper.Map<Brand>(model);
            brand.UpdatedAt = DateTime.Now;
            brand.CreatedAt = DateTime.Now;

            await _unitOfWork.Brands.AddAsync(brand);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<AddBrandDTO>(brand);
        }

        public async Task<bool> DeleteBrand(int Id)
        {
            var brand = await _unitOfWork.Brands.GetByIdAsync(Id);
            if (brand == null)
            {
                throw new Exception("Brand not Found");
            }

            _unitOfWork.Brands.Remove(brand);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<List<FetchBrandDTO>> GetAllBrands()
        {
            try
            {
                var brand = await _unitOfWork.Brands.GetAllAsync();
                if (brand == null)
                {
                    throw new Exception("Brand not Found");
                }
                return _mapper.Map<List<FetchBrandDTO>>(brand).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Brand not Found");
            }
        }

        public async Task<FetchBrandDTO> GetByIdBrands(int Id)
        {
            try
            {
                var brand = await _unitOfWork.Brands.GetByIdAsync(Id);
                if (brand == null)
                {
                    throw new Exception("Brand not Found");
                }
                return _mapper.Map<FetchBrandDTO>(brand);
            }
            catch (Exception ex)
            {
                throw new Exception("Brand not Found");
            }
        }

        public async Task<UpdateBrandDTO> UpdateBrand(UpdateBrandDTO model, int Id)
        {
            var brand = await _unitOfWork.Brands.GetByIdAsync(Id);
            if (brand == null)
            {
                throw new Exception("Brand not Found");
            }
            brand.BrandName = model.BrandName;
            brand.Discription = model.Discription ?? "";

            _unitOfWork.Brands.Update(brand);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<UpdateBrandDTO>(brand);
        }
    }
}
