using AutoMapper;
using Mango.Services.ShoppingCartAPI.Models;


namespace Mango.Services.ShoppingCartAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingconfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CartHeader,CartHeaderDto>();
                config.CreateMap<CartDetails, CartDetailsDto>();

            }
            );
            return mappingconfig;

        }
    }
}
