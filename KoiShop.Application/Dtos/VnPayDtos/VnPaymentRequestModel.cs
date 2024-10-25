using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos.VnPayDtos
{
    public class VnPaymentRequestModel
    {
        public double Amount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
