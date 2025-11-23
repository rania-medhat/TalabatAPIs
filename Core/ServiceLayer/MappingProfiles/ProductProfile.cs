using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models;
using Shared.DTOs;

namespace ServiceLayer.MappingProfiles
{
    public class ProductProfile :Profile
    {
        public ProductProfile(){
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.BrandName, options => options.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.BrandName, options => options.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.PictureUrl, options => options.MapFrom<PictureUrlResolver>());

            CreateMap<ProductBrand, BrandDTO>();
            CreateMap<ProductType, TypeDTO>();
        }
    }
}
