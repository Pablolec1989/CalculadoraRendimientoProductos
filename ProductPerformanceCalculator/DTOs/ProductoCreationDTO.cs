using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ProductPerformanceCalculator.DTOs
{
    public class ProductoCreationDTO
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public IFormFile? Foto { get; set; }
        public required decimal PresentacionEnLitros { get; set; }
        public required decimal DilucionDeUsoMaxima { get; set; }
        public decimal Precio { get; set; }
    }
}
