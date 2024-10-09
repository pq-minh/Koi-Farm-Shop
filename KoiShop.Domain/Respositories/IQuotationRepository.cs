﻿using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Domain.Respositories
{
    public interface IQuotationRepository
    {
        Task<Quotation> UpdatePriceQuotation(Quotation quotation);
    }
}
