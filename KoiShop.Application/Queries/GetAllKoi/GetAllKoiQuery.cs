using KoiShop.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Queries.GetAllKoi
{
    public class GetAllKoiQuery : IRequest<IEnumerable<Koi>>
    {

    }
}
