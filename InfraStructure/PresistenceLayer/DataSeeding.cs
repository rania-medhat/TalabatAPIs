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
        public async Task DataSeedAsync()
        {
            try
            {
                //production
                if ((await _storeDbContext.Database.GetPendingMigrationsAsync()).Any())
                {
                    await _storeDbContext.Database.MigrateAsync();
                }

                if (!_storeDbContext.ProductBrand.Any())
                {
                    //var productBrandsData = await File.ReadAllTextAsync(@"..\InfraStructure\PresistenceLayer\Data\DataSeed\brands.json");
                    var productBrandsData =  File.OpenRead(@"..\InfraStructure\PresistenceLayer\Data\DataSeed\brands.json");
                    //convert string to c# object
                    var brands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productBrandsData);
                    if (brands is not null && brands.Any())
                    {
                        await _storeDbContext.ProductBrand.AddRangeAsync(brands);
                    }
                }

                if (!_storeDbContext.ProductType.Any())
                {
                    var productTypesData = File.OpenRead(@"..\InfraStructure\PresistenceLayer\Data\DataSeed\types.json");
                    //convert string to c# object
                    var types =await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypesData);
                    if (types is not null && types.Any())
                    {
                        await _storeDbContext.ProductType.AddRangeAsync(types);
                    }
                }

                if (!_storeDbContext.Products.Any())
                {
                    var ProductsData = File.OpenRead(@"..\InfraStructure\PresistenceLayer\Data\DataSeed\products.json");
                    //convert string to c# object
                    var products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductsData);
                    if (products is not null && products.Any())
                    {
                        await _storeDbContext.Products.AddRangeAsync(products);
                    }
                }

                await _storeDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                //throw;
            }
        }
    }
}
