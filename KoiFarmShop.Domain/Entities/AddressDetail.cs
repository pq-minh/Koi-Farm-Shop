using System;
using System.Collections.Generic;

namespace KoiFarmShop.Domain.Entities
{

    public partial class AddressDetail
    {
        public int AddressId { get; set; }

        public string? City { get; set; }

        public string? Dictrict { get; set; }

        public string? UserId { get; set; }

        public string? StreetName { get; set; }

        public virtual AspNetUser? User { get; set; }
    }
}