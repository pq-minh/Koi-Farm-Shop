using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos
{
    public class RequestCareDtos
    {
        public int RequestId { get; set; }
        public int? PackageId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ConsignmentDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? TypeRequest { get; set; }
        public string? Status { get; set; }
        public int? KoiId { get; set; }
        public string? KoiName { get; set; }
        public string? KoiImage { get; set; }
        public int? BatchKoiId { get; set; }
        public string? BatchKoiName { get; set; }
        public string? BatchKoiImage { get; set; }

    }
    public class RequestCareDtoV1
    {
        public List<OrderDetailDtoV1> OrderDetails { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class RequestCareDtoV2
    {
        public int? Id { get; set; }
        public int? Status { get; set; }
    }
}
