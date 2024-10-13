using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Domain.Entities
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public int? Point { get; set; }
        public string? Status { get; set; }
        public virtual ICollection<AddressDetail> AddressDetails { get; set; } = new List<AddressDetail>();
        public virtual ICollection<Quotation> Quotations{ get; set; } = new List<Quotation>();

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();

        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

        public virtual ICollection<Koi> Kois { get; set; } = new List<Koi>();

        public virtual ICollection<BatchKoi> BatchKois { get; set; } = new List<BatchKoi>();

      
    }
}
