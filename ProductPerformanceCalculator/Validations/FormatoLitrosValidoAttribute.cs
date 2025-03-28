using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ProductPerformanceCalculator.Validations
{
    public class FormatoLitrosValidoAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Los litros son requeridos.");
            }

            string litrosString = value.ToString();

            // Expresión regular para validar el formato "número.númeroL"
            Regex regex = new Regex(@"^\d+(\.\d{1,3})?[Ll]$");

            if (!regex.IsMatch(litrosString))
            {
                return new ValidationResult("El formato de litros no es válido. Debe ser 'número.númeroL' (ej. 2.500L).");
            }

            return ValidationResult.Success;
        }
    }
}
