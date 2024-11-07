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
        public required int PaymentId;

        [Required(ErrorMessage = "PaymentStatus is required.")]
        public required string PaymentStatus;
    }
}
