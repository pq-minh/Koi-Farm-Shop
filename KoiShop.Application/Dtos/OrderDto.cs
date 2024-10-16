using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos
{
    public class OrderDto
    {
        public int? OrderDetailsId { get; set; }

        public int? OrderId { get; set; }

        public int? KoiId { get; set; }

        public int? BatchKoiId { get; set; }

        public int? ToTalQuantity { get; set; }
    }
    public class OrderDtoV1
    {
        public List<CartDtoV2> Carts { get; set; }
        public string Method { get; set; }
    }
}
