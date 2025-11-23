using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTOs;

namespace ServiceAbstractionLayer
{
    public interface IProductService //abstraction for service layer
    {
        //get all products
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        //get product by id
        Task<ProductDTO> GetProductByIdAsync(int id);
        //get all types
        Task<IEnumerable<TypeDTO>> GetAllTypesAsync();
        //get all brands
        Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();
    }
}
