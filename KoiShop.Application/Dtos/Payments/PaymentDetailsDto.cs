using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos.Payments
{
    public class PaymentDetailsDto
    {
        public int PaymentID { get; set; }
        public DateTime CreateDate { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public int OrderID { get; set; }
        public double TotalAmount { get; set; }
        public string UserId { get; set; }
    }

}
