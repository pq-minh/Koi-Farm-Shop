using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Domain.Entities
{
    public class Payment
    {
        public int? PaymentID { get; set; }
        public DateOnly ? CreateDate { get; set; }

        public string? PaymenMethod { get; set;}

        public string? Status { get; set; }
        public int? OrderId { get; set; }

        public virtual Order? Order { get; set; }

    }
}
