using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Crud_App_dotNetApplication.DTOs.BrandDTOs;
using Crud_App_dotNetApplication.DTOs.CategoryDTOs;
using Crud_App_dotNetApplication.DTOs.ProductDTOs;
using Crud_App_dotNetCore.Entities;

namespace Crud_App_dotNetApplication.Mapping
{
    public class MappinProfiles : Profile
    {
        public MappinProfiles()
        {
            CreateMap<CategoryDTO, Category>()
                 .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Product, FetchProductDTO>();
            //         .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()) // Optional: preserve original value
            //.ForMember(dest => dest.UpdatedDate, opt => opt.Ignore());

            CreateMap<FetchProductDTO, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<FetchProductDTO, Product>();

            // Optional separate DTOs for creation/update
            CreateMap<DTOs.ProductDTOs.AddProductDTO, Product>();
            CreateMap<DTOs.ProductDTOs.UpdateProductDTO, Product>();

            CreateMap<CategoryDTO, Category>();

            CreateMap<CategoryDTO, Category>();

            CreateMap<Category, CategoryDTO>();
            CreateMap<Brand, FetchBrandDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.BrandName))
                .ForMember(dest => dest.Discription, opt => opt.MapFrom(src => src.Discription));
            CreateMap<Brand, AddBrandDTO>();
            //.ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.BrandName))
            //.ForMember(dest => dest.Discription, opt => opt.MapFrom(src => src.Discription));
            CreateMap<AddBrandDTO, Brand>();


            CreateMap<UpdateBrandDTO, Brand>().ReverseMap();


        }
    }
}
