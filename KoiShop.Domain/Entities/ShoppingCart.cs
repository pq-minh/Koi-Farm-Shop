﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Domain.Entities
{
    public class ShoppingCart
    {
        public int ShoppingCartID { get; set; }
        public DateTime? CreateDate { get; set; }
        public virtual User? User { get; set; }
        public string? UserId { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    }
}
