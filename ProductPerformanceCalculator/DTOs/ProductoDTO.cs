using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ProductPerformanceCalculator.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Foto { get; set; }
        public decimal PresentacionEnLitros { get; set; }
        public required decimal DilucionDeUsoMaxima { get; set; }
        public decimal RendimientoPorLitro { get; set; }
        public decimal Precio { get; set; }
    }
}
