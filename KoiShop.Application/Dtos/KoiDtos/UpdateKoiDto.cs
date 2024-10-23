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
        public int? FishTypeId { get; set; }


        [StringLength(100, MinimumLength = 2, ErrorMessage = "Koi name must be between 2 and 100 characters.")]
        public string? KoiName { get; set; }


        [StringLength(100, ErrorMessage = "Origin cannot exceed 100 characters.")]
        public string? Origin { get; set; }


        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string? Description { get; set; }


        [StringLength(50, ErrorMessage = "Gender cannot exceed 50 characters.")]
        public string? Gender { get; set; }


        [Range(0, 100, ErrorMessage = "Age must be between 0 and 100.")]
        public int? Age { get; set; }


        [Range(0.1, 100.0, ErrorMessage = "Weight must be between 0.1 and 100.0 kg.")]
        public double? Weight { get; set; }


        [Range(0.1, 300.0, ErrorMessage = "Size must be between 0.1 and 300.0 cm.")]
        public double? Size { get; set; }


        [StringLength(200, ErrorMessage = "Personality description cannot exceed 200 characters.")]
        public string? Personality { get; set; }


        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public string? Status { get; set; }


        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public double? Price { get; set; }


        [MaxFileSize(5 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is 5 MB.")]
        [AllowedFileTypes(".jpg,.jpeg,.png", ErrorMessage = "Only image files (jpg, jpeg, png) are allowed.")]
        public IFormFile? KoiImage { get; set; }


        [MaxFileSize(5 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is 5 MB.")]
        [AllowedFileTypes(".pdf,.jpg,.jpeg,.png", ErrorMessage = "Only certificate files (pdf, jpg, jpeg, png) are allowed.")]
        public IFormFile? Certificate { get; set; }


    }
}
