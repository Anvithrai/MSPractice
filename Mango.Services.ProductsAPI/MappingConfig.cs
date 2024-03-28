using AutoMapper;
using Mango.Services.ProductsAPI.Models;
using Mango.Services.ProductsAPI.Models.Dto;


namespace Mango.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingconfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>();
                config.CreateMap<Product, ProductDto>();

            }
            );
            return mappingconfig;

        }
    }
}
