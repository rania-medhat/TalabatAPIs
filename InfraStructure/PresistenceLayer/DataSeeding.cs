using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using PresistenceLayer.Data;

namespace PresistenceLayer
{
    public class DataSeeding (StoreDBContext _storeDbContext): IDataSeeding
    {
        public void DataSeed()
        {
            try
            {
                if (_storeDbContext.Database.GetPendingMigrations().Any())
                {
                    _storeDbContext.Database.Migrate();
                }

                if (!_storeDbContext.ProductBrand.Any())
                {
                    var productBrandsData = File.ReadAllText(@"..\InfraStructure\PresistenceLayer\Data\DataSeed\brands.json");
                    //convert string to c# object
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrandsData);
                    if (brands is not null && brands.Any())
                    {
                        _storeDbContext.ProductBrand.AddRange(brands);
                    }
                }

                if (!_storeDbContext.ProductType.Any())
                {
                    var productTypesData = File.ReadAllText(@"..\InfraStructure\PresistenceLayer\Data\DataSeed\types.json");
                    //convert string to c# object
                    var types = JsonSerializer.Deserialize<List<ProductType>>(productTypesData);
                    if (types is not null && types.Any())
                    {
                        _storeDbContext.ProductType.AddRange(types);
                    }
                }

                if (!_storeDbContext.Products.Any())
                {
                    var ProductsData = File.ReadAllText(@"..\InfraStructure\PresistenceLayer\Data\DataSeed\products.json");
                    //convert string to c# object
                    var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                    if (products is not null && products.Any())
                    {
                        _storeDbContext.Products.AddRange(products);
                    }
                }

                _storeDbContext.SaveChanges();
            }
            catch (Exception)
            {

                //throw;
            }
        }
    }
}
