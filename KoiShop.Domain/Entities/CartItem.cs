using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Domain.Entities
{
    public class CartItem
    {
        public int CartItemsID { get; set; }
        public int? Quantity { get; set; }
        public float? UnitPrice { get; set; }
        public float? TotalPrice { get; set; }
        public string? Status { get; set; }
        public int? KoiId { get; set; }

        public int? BatchKoiId { get; set; }

        public int? ShoppingCartId { get; set; }
        public virtual BatchKoi? BatchKoi { get; set; }

        public virtual Koi? Koi { get; set; }

        public virtual ShoppingCart? ShoppingCart { get; set; }
    }
}
