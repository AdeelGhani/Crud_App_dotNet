using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Crud_App_dotNetApplication.DTOs.CategoryDTOs;
using Crud_App_dotNetApplication.DTOs.ProductDTOs;
using Crud_App_dotNetCore.Entities;

namespace Crud_App_dotNetApplication.Mapping
{
    public class MappinProfiles :Profile
    {
        public MappinProfiles()
        {
            CreateMap<Product, FetchProductDTO>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
            CreateMap<AddProductDTO, Product>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ProductDescription))
                .ForMember(dest => dest.CategoryId,opt => opt.MapFrom(src=>src.CategoryId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.StockQuantity))
                .ForMember(dest => dest.SKU, opt => opt.MapFrom(src => src.SKU))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));
            CreateMap<UpdateProductDTO, Product>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ProductDescription))
                .ForMember(dest => dest.CategoryId,opt => opt.MapFrom(src=>src.CategoryId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.StockQuantity))
                .ForMember(dest => dest.SKU, opt => opt.MapFrom(src => src.SKU))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));

            CreateMap<AddCategoryDTO, Category>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));
            CreateMap<UpdateCategoryDTO, Category>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.CategoryDescription))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));
            CreateMap<Category, FetchCategoryDTO>();
        }
    }
}
