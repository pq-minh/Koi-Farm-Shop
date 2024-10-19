using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos.KoiDtos
{
    public class AddBatchKoiDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string BatchKoiName { get; set; }


        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Age is required.")]
        [StringLength(50, ErrorMessage = "Age cannot exceed 50 characters.")]
        public string Age { get; set; }


        [Required(ErrorMessage = "Quantity is required.")]
        [StringLength(50, ErrorMessage = "Quantity cannot exceed 50 characters.")]
        public string Quantity { get; set; }


        [Required(ErrorMessage = "Weight is required.")]
        [StringLength(50, ErrorMessage = "Weight cannot exceed 50 characters.")]
        public string Weight { get; set; }


        [Required(ErrorMessage = "Size is required.")]
        [StringLength(50, ErrorMessage = "Size cannot exceed 50 characters.")]
        public string Size { get; set; }


        [Required(ErrorMessage = "Origin is required.")]
        [StringLength(100, ErrorMessage = "Origin cannot exceed 100 characters.")]
        public string Origin { get; set; }


        [Required(ErrorMessage = "Gender is required.")]
        [RegularExpression("^(Male|Female|Unknown)$", ErrorMessage = "Gender must be 'Male', 'Female', or 'Unknown'.")]
        public string Gender { get; set; }


        [Required(ErrorMessage = "Certificate file is required.")]
        [FileExtensions(Extensions = "pdf,jpg,jpeg,png", ErrorMessage = "Only certificate files (pdf, jpg, jpeg, png) are allowed.")]
        [MaxFileSize(5 * 1024 * 1024, ErrorMessage = "Certificate file size cannot exceed 5MB.")]
        public IFormFile Certificate { get; set; }


        [Required(ErrorMessage = "Image file is required.")]
        [FileExtensions(Extensions = "jpg,jpeg,png", ErrorMessage = "Only koi image files (jpg, jpeg, png) are allowed.")]
        [MaxFileSize(5 * 1024 * 1024, ErrorMessage = "Image file size cannot exceed 5MB.")]
        public IFormFile KoiImage { get; set; }


        [Required(ErrorMessage = "Batch type is required.")]
        public int BatchTypeId { get; set; }


        [Required(ErrorMessage = "Price is required.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public double Price { get; set; }


        [Required(ErrorMessage = "Status is required.")]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public string Status { get; set; }

    }

}
