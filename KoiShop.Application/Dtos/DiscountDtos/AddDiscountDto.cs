using KoiShop.Application.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos.DiscountDtos
{
    public class AddDiscountDto
    {

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters.")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Description is required.")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Discount rate is required.")]
        [Range(0, 100, ErrorMessage = "Discount rate must be between 0 and 100.")]
        public double DiscountRate { get; set; }


        [Required(ErrorMessage = "Total quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Total quantity must be at least 1.")]
        public int TotalQuantity { get; set; }


        [Required(ErrorMessage = "Used quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Used quantity cannot be negative.")]
        public int Used { get; set; }


        [Required(ErrorMessage = "Create date is required.")]
        public DateTime CreateDate { get; set; }


        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }


        [Required(ErrorMessage = "End date is required.")]
        [DateGreaterThan("StartDate", ErrorMessage = "End date must be greater than start date.")]
        public DateTime EndDate { get; set; }


        [Required(ErrorMessage = "Status is required.")]
        public int Status { get; set; }

    }

}
