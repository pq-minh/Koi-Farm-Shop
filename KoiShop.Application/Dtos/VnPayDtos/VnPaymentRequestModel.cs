using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos.VnPayDtos
{
    public class VnPaymentRequestModel
    {
        public int UserID { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public int OrderId { get; set; }
    }
}
