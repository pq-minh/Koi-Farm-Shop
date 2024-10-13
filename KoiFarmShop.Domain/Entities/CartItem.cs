using KoiFarmShop.Domain.Entities;
using System;
using System.Collections.Generic;

namespace KoiFarmShop.Domain.Entities
{

    public partial class CartItem
    {
        public int CartItemsId { get; set; }

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