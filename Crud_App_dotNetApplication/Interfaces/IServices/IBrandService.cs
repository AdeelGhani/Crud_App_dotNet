using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud_App_dotNetApplication.DTOs.BrandDTOs;

namespace Crud_App_dotNetApplication.Interfaces.IServices
{
    public interface IBrandService
    {
        Task<List<FetchBrandDTO>> GetAllBrands();
        Task<FetchBrandDTO> GetByIdBrands(int Id);
        Task<AddBrandDTO> CreateBrand(AddBrandDTO model);
        Task<UpdateBrandDTO> UpdateBrand(UpdateBrandDTO model, int Id);
        Task<bool> DeleteBrand(int Id);
    }
}
