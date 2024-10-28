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
    public class AddKoiDto
    {
        [Required(ErrorMessage = "Fish type is required.")]
        public int FishTypeId { get; set; }


        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Origin is required.")]
        public string Origin { get; set; }


        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }


        [Required(ErrorMessage = "Age is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Age must be greater than 0.")]
        public int Age { get; set; }


        [Required(ErrorMessage = "Weight is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Weight must be greater than 0.")]
        public double Weight { get; set; }


        [Required(ErrorMessage = "Size is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Size must be greater than 0.")]
        public double Size { get; set; }


        [Required(ErrorMessage = "Personality is required.")]
        public string Personality { get; set; }


        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }


        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public double Price { get; set; }


        [Required(ErrorMessage = "Koi Image is required.")]
        [MaxFileSize(5 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is 5 MB.")]
        [AllowedFileTypes(".jpg,.jpeg,.png", ErrorMessage = "Only image files (jpg, jpeg, png) are allowed.")]
        public IFormFile KoiImage { get; set; }


        [Required(ErrorMessage = "Certificate file is required.")]
        [MaxFileSize(5 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is 5 MB.")]
        [AllowedFileTypes(".pdf,.jpg,.jpeg,.png", ErrorMessage = "Only certificate files (pdf, jpg, jpeg, png) are allowed.")]
        public IFormFile Certificate { get; set; }  


    }

}
