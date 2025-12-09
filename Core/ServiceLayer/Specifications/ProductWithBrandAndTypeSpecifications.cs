using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace ServiceLayer.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product,int>
    {
        // Nullable criteria
        //get all products with type and brand
        public ProductWithBrandAndTypeSpecifications(int? brandId, int? typeId) 
            :base(p=>(!brandId.HasValue || p.BrandId==brandId)
                   &&(!typeId.HasValue || p.TypeId == typeId))
        {//where(p=>p.BrandId==brandId && TypeId==typeId)
            //specifying
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            
            
        }
        //get product by id with type and brand
        public ProductWithBrandAndTypeSpecifications(int id)
            : base(p => p.Id == id)
        {
            //specifying
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
}
}
