using Microsoft.EntityFrameworkCore;
using ProductPerformanceCalculator.Validations;
using System.ComponentModel.DataAnnotations;

namespace ProductPerformanceCalculator.Entities
{
    public class Producto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} debe existir")]
        [MaxLength(50)]
        public required string Nombre { get; set; }
        [MaxLength(500)]
        public string? Descripcion { get; set; }
        [Unicode(false)]
        public string? Foto { get; set; }
        [Required]
        [FormatoLitrosValido]
        public required decimal PresentacionEnLitros { get; set; }
        [Required]
        [FormatoPrecioValido(ErrorMessage = "Formato de precio inválido.")]
        public decimal Precio { get; set; }

        [Required]
        [Range(0.0001, double.MaxValue, ErrorMessage = "La dilución debe ser mayor a 0")]
        public required decimal DilucionDeUsoMaxima { get; set; }

        // Propiedad calculada
        public decimal RendimientoPorLitro => PresentacionEnLitros*1000 / DilucionDeUsoMaxima;
        public decimal PrecioPorLitro => Precio / RendimientoPorLitro;

    }
}
