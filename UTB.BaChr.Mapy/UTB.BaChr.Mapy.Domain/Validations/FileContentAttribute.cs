using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace UTB.BaChr.Mapy.Domain.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class FileContentAttribute : ValidationAttribute
    {
        private readonly string[] _allowedTypes;

        public FileContentAttribute(params string[] allowedTypes)
        {
            _allowedTypes = allowedTypes;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (value is IFormFile file)
            {
                // Kontrola typu souboru (MIME type)
                if (!_allowedTypes.Contains(file.ContentType))
                {
                    return new ValidationResult($"Typ souboru {file.ContentType} není povolen. Povolené jsou: {string.Join(", ", _allowedTypes)}");
                }

                // Kontrola velikosti (např. max 5MB)
                if (file.Length > 5 * 1024 * 1024)
                {
                    return new ValidationResult("Soubor je příliš velký. Maximum je 5 MB.");
                }
            }

            return ValidationResult.Success;
        }
    }
}