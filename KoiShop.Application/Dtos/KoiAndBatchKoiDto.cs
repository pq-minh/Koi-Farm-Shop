using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos
{
    public class KoiAndBatchKoiDto
    {
        public IEnumerable<KoiDto>? Koi { get; set; }
        public IEnumerable<BatchKoiDto>? BatchKoi { get; set; }
    }
    public class KoiAndBatchKoiIdDto
    {
        public KoiDto? Koi { get; set; }
        public BatchKoiDto? BatchKoi { get; set; }
    }
}
