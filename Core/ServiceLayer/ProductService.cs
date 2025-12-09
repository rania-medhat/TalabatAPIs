using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using ServiceAbstractionLayer;
using ServiceLayer.Specifications;
using Shared.DTOs;


namespace ServiceLayer
{
    public class ProductService (IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService //implementation of service abstraction layer
    {
       

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync(int? brandId, int? typeId)
        {
            //create object from specification
            
            var specs = new ProductWithBrandAndTypeSpecifications(brandId,typeId);//.include(p=>p.ProductBrand).include(p=>p.ProductType)
            
            var products =await _unitOfWork.GetRepository<Product, int>().GetAllAsync(specs);
            var productsDTO = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return productsDTO;

        }
       public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var specs = new ProductWithBrandAndTypeSpecifications(id);//.include(p=>p.ProductBrand).include(p=>p.ProductType)


            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(specs);
            var productDTO = _mapper.Map<ProductDTO>(product);
            return productDTO;
        }
        #region Types and Brands
        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();

            return _mapper.Map<IEnumerable<TypeDTO>>(types);
        }
        public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {
            var repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var brands = await repo.GetAllAsync();
            var brandsDTO = _mapper.Map<IEnumerable<BrandDTO>>(brands);
            return brandsDTO;

        } 
        #endregion

    }
}
