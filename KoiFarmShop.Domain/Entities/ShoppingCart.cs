using System;
using System.Collections.Generic;

namespace KoiFarmShop.Domain.Entities
{

    public partial class ShoppingCart
    {
        public int ShoppingCartId { get; set; }

        public DateOnly? CreateDate { get; set; }

        public string? UserId { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        public virtual AspNetUser? User { get; set; }
    }
}