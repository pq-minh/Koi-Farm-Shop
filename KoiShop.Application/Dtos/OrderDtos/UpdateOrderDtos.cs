using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos.OrderDtos
{
    public class UpdateOrderDtos
    {
        [Required(ErrorMessage = "OrderId is required.")]
        public int OrderId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "TotalAmount must be greater than 0.")]
        public float? TotalAmount { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? OrderStatus { get; set; }

        public int? DiscountId { get; set; }

        public string? ShippingAddress { get; set; }


        [RegularExpression(@"^\d{10}$", ErrorMessage = "PhoneNumber must be exactly 10 digits.")]
        public string? PhoneNumber { get; set; }  
    }
    public class OrderDtoUpdateStatus
    {
       public int  OrderID { get; set; }
       public string? Status { get; set; }
    }
}
