using System;
using System.Collections.Generic;

namespace KoiShop.Domain.Entities;

public partial class AddressDetail
{
    public int AddressId { get; set; }

    public string? City { get; set; }

    public string? Dictrict { get; set; }
    public string? UserId { get; set; }
    public virtual User? User { get; set; }
    public string? StreetName { get; set; }
    public string? Ward {  get; set; }
}
