using AutoMapper;
using Microsoft.Identity.Client;
using ProductPerformanceCalculator.DTOs;
using ProductPerformanceCalculator.Entities;

namespace ProductPerformanceCalculator.Utilities
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            ConfigurarMapeoProductos();
            ConfigurarMapeoProductosPropios();
        }

        private void ConfigurarMapeoProductos()
        {
            CreateMap<Producto, ProductoDTO>();
            CreateMap<ProductoCreationDTO, Producto>();
        }

        private void ConfigurarMapeoProductosPropios()
        {
            CreateMap<Producto, ProductoPropioDTO>();
            CreateMap<ProductoPropioCreationDTO, Producto>();
        }
    }
}
