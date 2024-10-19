using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Validation
{
    public class AllowedFileTypesAttribute : ValidationAttribute
    {
        private readonly string[] _allowedExtensions;

        public AllowedFileTypesAttribute(string allowedExtensions)
        {
            _allowedExtensions = allowedExtensions.Split(',');
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

                if (Array.IndexOf(_allowedExtensions, extension) < 0)
                {
                    return new ValidationResult($"Only files with the following extensions are allowed: {string.Join(", ", _allowedExtensions)}.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
