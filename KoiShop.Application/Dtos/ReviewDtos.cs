using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos
{
    public class ReviewDtos
    {
        public int ReviewId { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? Rating { get; set; }

        public string? Comments { get; set; }

        public int? KoiId { get; set; }

        public int? BatchKoiId { get; set; }
        public string? UserId { get; set; }
    }
    public class ReviewDtoV1
    {
        public int? Id { get; set; }
    }
}
