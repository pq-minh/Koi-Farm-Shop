using KoiShop.Application.Dtos.VnPayDtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Interfaces
{
    public interface IVnPayService
    {
        Task<string> CreatePatmentUrl(HttpContext content, VnPaymentRequestModel paymentRequestModel);
        VnPaymentResponseModel ExecutePayment(VnPaymentResponseFromFe request);
        VnPaymentRequestModel CreateVnpayModel(VnPaymentRequestModel paymentRequest);
    }
}
