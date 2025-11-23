using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstractionLayer;
using Shared.DTOs;

namespace PresentationLayer
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController(IServiceManager _serviceManager):ControllerBase
    {
        //Get all products
        [HttpGet] //Get::BaseURL/api/products
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            var products = await _serviceManager.ProductService.GetAllProductsAsync();
            return Ok(products);
        }
        //Get product by id
        [HttpGet("{id}")] //Get::BaseURL/api/products/4
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }
        //Get all brands
        [HttpGet("brands")] //Get::BaseURL/api/products/brands
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetAllBrands()
        {
            var brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(brands);
        }
        //Get all types
        [HttpGet("types")] //Get::BaseURL/api/products/types
        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetAllTypes()
        {
            var types = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(types);
        }
    }
}
