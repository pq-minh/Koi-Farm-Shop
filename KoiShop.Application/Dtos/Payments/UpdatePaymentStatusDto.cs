using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos.Payments
{
    public class UpdatePaymentStatusDto
    {
        [Required(ErrorMessage = "PaymentId is required.")]
        public int PaymentId { get; set; }

        [Required(ErrorMessage = "PaymentStatus is required.")]
        public string PaymentStatus { get; set; }
    }
}
