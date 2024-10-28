using KoiShop.Application.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos.KoiDtos
{
    public class UpdateKoiDto
    {

        [Required(ErrorMessage = "KoiId is required.")]
        public int KoiId { get; set; }

        public int? FishTypeId { get; set; }


        public string? Name { get; set; }


        public string? Origin { get; set; }


        public string? Description { get; set; }


        public string? Gender { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "Age must be greater than 0.")]
        public int? Age { get; set; }


        [Range(0.01, double.MaxValue, ErrorMessage = "Weight must be greater than 0.")]
        public double? Weight { get; set; }


        [Range(0.01, double.MaxValue, ErrorMessage = "Size must be greater than 0.")]
        public double? Size { get; set; }


        public string? Personality { get; set; }


        public string? Status { get; set; }


        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public double? Price { get; set; }


        [MaxFileSize(5 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is 5 MB.")]
        [AllowedFileTypes(".jpg,.jpeg,.png", ErrorMessage = "Only image files (jpg, jpeg, png) are allowed.")]
        public IFormFile? KoiImage { get; set; }


        [MaxFileSize(5 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is 5 MB.")]
        [AllowedFileTypes(".pdf,.jpg,.jpeg,.png", ErrorMessage = "Only certificate files (pdf, jpg, jpeg, png) are allowed.")]
        public IFormFile? Certificate { get; set; }

    }
}
