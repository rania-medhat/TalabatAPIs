using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;
using Shared.DTOs;

namespace ServiceLayer.MappingProfiles
{
    internal class PictureUrlResolver(IConfiguration _configuration /* microsoft.extention.configuration extention for appsettings urls*/) : IValueResolver<Product, ProductDTO, string>
    {
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source.PictureUrl))//empty
                return string.Empty;
            else
            {
                var url =  
                    $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.PictureUrl}";
                return url;
            }
        }
    }
}
