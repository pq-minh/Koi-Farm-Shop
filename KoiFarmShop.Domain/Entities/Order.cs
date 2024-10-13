using System;
using System.Collections.Generic;

namespace KoiFarmShop.Domain.Entities
{

    public partial class Order
    {
        public int OrderId { get; set; }

        // đổi TotalAmount và CreateDate
        public float? TotalAmount { get; set; }
        public DateTime? CreateDate { get; set; }

        public string? OrderStatus { get; set; }

        public int? DiscountId { get; set; }

        //public string? PaymentMethod { get; set; }

        //public string? PaymentStatus { get; set; }

        public string? UserId { get; set; }

        public virtual Discount? Discount { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public virtual AspNetUser? User { get; set; }
    }
}