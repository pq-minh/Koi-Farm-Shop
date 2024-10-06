using KoiShop.Application.Koi.Dtos;
using KoiShop.Application.Package.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.ViewModel.KoiPackageViewModel
{
    public class KoiPackage
    {
        public KoiDto? Koi { get; set; }
        public ICollection<PackageDto> Packages { get; set; } = new List<PackageDto>();
    }
}
