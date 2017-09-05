using AutoMapper;
using Refaction.Core.Dtos;
using Refaction.Core.Models;

namespace Refaction
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            // Creates mapping for Domain Models and Data Transfer Objects
            Mapper.Initialize((config) =>
            {
                config.CreateMap<Product, ProductDto>().ReverseMap();
                config.CreateMap<ProductOption, ProductOptionDto>().ReverseMap();
            });
        }
    }
}