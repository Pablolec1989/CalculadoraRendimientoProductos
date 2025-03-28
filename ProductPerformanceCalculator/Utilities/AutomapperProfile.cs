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
        }

        private void ConfigurarMapeoProductos()
        {
            CreateMap<Producto, ProductoDTO>();
            CreateMap<ProductoCreationDTO, Producto>();
        }
    }
}
