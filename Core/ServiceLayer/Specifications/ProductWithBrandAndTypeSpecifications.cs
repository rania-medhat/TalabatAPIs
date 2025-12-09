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
        public ProductWithBrandAndTypeSpecifications():base(null)
        {
            //specifying
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            
            
        }
    }
}
