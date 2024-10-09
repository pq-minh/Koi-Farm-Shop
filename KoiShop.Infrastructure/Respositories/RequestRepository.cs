using Azure.Core;
using KoiShop.Application.Command.CreateRequest;
using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using KoiShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Request = KoiShop.Domain.Entities.Request;

namespace KoiShop.Infrastructure.Respositories
{
    public class RequestRepository(KoiShopV1DbContext koiShopV1DbContext) : IRequestRepository
    {
        public async Task<Koi> CreateRequest(Koi entity)
        {
           await koiShopV1DbContext.Kois.AddAsync(entity);
            await koiShopV1DbContext.SaveChangesAsync();
            return entity;
        }
    }
}
