using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos.KoiDtos
{
    public class UpdateBatchKoiDto
    {

        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string? BatchKoiName { get; set; }


        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }


        [StringLength(50, ErrorMessage = "Age cannot exceed 50 characters.")]
        public string? Age { get; set; }


        [StringLength(50, ErrorMessage = "Quantity cannot exceed 50 characters.")]
        public string? Quantity { get; set; }


        [StringLength(50, ErrorMessage = "Weight cannot exceed 50 characters.")]
        public string? Weight { get; set; }


        [StringLength(50, ErrorMessage = "Size cannot exceed 50 characters.")]
        public string? Size { get; set; }


        [StringLength(100, ErrorMessage = "Origin cannot exceed 100 characters.")]
        public string? Origin { get; set; }


        [RegularExpression("^(Male|Female|Unknown)$", ErrorMessage = "Gender must be 'Male', 'Female', or 'Unknown'.")]
        public string? Gender { get; set; }


        [FileExtensions(Extensions = "pdf,jpg,jpeg,png", ErrorMessage = "Only certificate files (pdf, jpg, jpeg, png) are allowed.")]
        [MaxFileSize(5 * 1024 * 1024, ErrorMessage = "Certificate file size cannot exceed 5MB.")]
        public IFormFile? Certificate { get; set; }


        [FileExtensions(Extensions = "jpg,jpeg,png", ErrorMessage = "Only koi image files (jpg, jpeg, png) are allowed.")]
        [MaxFileSize(5 * 1024 * 1024, ErrorMessage = "Image file size cannot exceed 5MB.")]
        public IFormFile? KoiImage { get; set; }


        [Required(ErrorMessage = "Batch type is required.")]
        public int? BatchTypeId { get; set; }


        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public double? Price { get; set; }


        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public string? Status { get; set; }
    }
}
