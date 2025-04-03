using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ProductPerformanceCalculator.Validations
{
    public class FormatoPrecioValidoAttribute : ValidationAttribute 
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("El precio es requerido.");
            }

            string? precioString = value.ToString();

            // Expresión regular para validar el formato "$número.número" sin decimales
            Regex regex = new Regex(@"^\$\d{1,3}(\.\d{3})*$");

            if (!regex.IsMatch(precioString!))
            {
                return new ValidationResult("El formato del precio no es válido. Debe ser ej. $73.500.");
            }

            return ValidationResult.Success;
        }
    }
}
