
using System;
using System.Collections.Generic;

namespace KoiFarmShop.Domain.Entities
{

    public partial class Koi
    {
        public int KoiId { get; set; }

        public int? FishTypeId { get; set; }

        public string? Name { get; set; }

        public string? Origin { get; set; }

        public string? Description { get; set; }

        public string? Gender { get; set; }

        public string? Image { get; set; }

        public int? Age { get; set; }

        public double? Weight { get; set; }

        public double? Size { get; set; }

        public string? Personality { get; set; }

        public string? Status { get; set; }

        public double? Price { get; set; }

        public string? Certificate { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        public virtual KoiCategory? FishType { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public virtual ICollection<Package> Packages { get; set; } = new List<Package>();

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
