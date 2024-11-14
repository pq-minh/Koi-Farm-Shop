﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos
{
    public class CartDtos
    {
        public int? CartItemsID { get; set; }
        public int? Quantity { get; set; }
        public float? UnitPrice { get; set; }
        public float? TotalPrice { get; set; }
        public string? Status { get; set; }
        public int? KoiId { get; set; }

        public int? BatchKoiId { get; set; }

        public int? ShoppingCartId { get; set; }

        public string? KoiName { get; set; }

        public string? BatchKoiName { get; set; }

        public string? KoiImgUrl {  get; set; }

        public string? BatchKoiImgUrl { get; set; }

        public string? KoiDescription { get; set; }

        public string? BatchKoiDescription { get;set; }
    }
    public class CartDtoV1
    {
        public int? KoiId { get; set; }
        public int? BatchKoiId { get; set; }
    }
    public class CartDtoV2
    {
        public int? KoiId { get; set; }
        public int? BatchKoiId { get; set; }
        public int? Quantity { get; set; }
    }
    public class CartDtoV3
    {
        public string? Status { get; set; }
        public int? BatchKoiId { get; set; }
    }
}
