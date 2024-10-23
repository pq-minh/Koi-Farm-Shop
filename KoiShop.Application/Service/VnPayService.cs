﻿using KoiShop.Application.Dtos.VnPayDtos;
using KoiShop.Application.Interfaces;
using KoiShop.Domain.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Service
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _config;
        private readonly IOrderRepository _orderRepository;

        public VnPayService(IConfiguration config, IOrderRepository orderRepository)
        {
            _config = config;
            _orderRepository = orderRepository;
        }
        public async Task<string> CreatePatmentUrl(HttpContext content, VnPaymentRequestModel paymentRequestModel)
        {
            var tick = DateTime.Now.Ticks.ToString();
            var orderId = await _orderRepository.GetCurentOrderId();
            var vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", _config["VnPay:Version"]);
            vnpay.AddRequestData("vnp_Command", _config["VnPay:Command"]);
            vnpay.AddRequestData("vnp_TmnCode", _config["VnPay:TmnCode"]);
            vnpay.AddRequestData("vnp_Amount", (paymentRequestModel.Amount * 100).ToString()); //Số tiền thanh toán. Số tiền không 
            //mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND
            //(một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần(khử phần thập phân), sau đó gửi sang VNPAY
            //là: 10000000

            vnpay.AddRequestData("vnp_CreateDate", paymentRequestModel.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", _config["VnPay:CurrCode"]);
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(content));
            vnpay.AddRequestData("vnp_Locale", _config["VnPay:Locale"]);
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + orderId);
            vnpay.AddRequestData("vnp_OrderType", "order"); //default value: other
            vnpay.AddRequestData("vnp_ReturnUrl", _config["VnPay:PaymentBackReturnUrl"]);
            vnpay.AddRequestData("vnp_TxnRef", tick); // Mã tham chiếu của giao dịch tại hệ 
                                                      //thống của merchant.Mã này là duy nhất dùng để
                                                      //phân biệt các đơn
                                                      //hàng gửi sang VNPAY.Không được
                                                      //  trùng lặp trong ngày
            var paymnentUrl = vnpay.CreateRequestUrl(_config["VnPay:BaseUrl"], _config["VnPay:HashSecret"]);
            return paymnentUrl;
        }

        public VnPaymentRequestModel CreateVnpayModel(VnPaymentRequestModel paymentRequest)
        {
            if (paymentRequest == null || paymentRequest.Amount <= 0)
            {
                return null;
            }

            var vnPayModel = new VnPaymentRequestModel
            {
                Amount = paymentRequest.Amount,
                CreatedDate = DateTime.Now,
            };
            return vnPayModel;
        }

        public VnPaymentResponseModel ExecutePayment(VnPaymentResponseFromFe request)
        {
            var vnpay = new VnPayLibrary();
            var responseData = new Dictionary<string, string>
    {
        { "vnp_Amount", request.Amount.ToString() },
        { "vnp_BankCode", request.BankCode },
        { "vnp_BankTranNo", request.BankTranNo },
        { "vnp_CardType", request.CardType },
        { "vnp_OrderInfo", request.OrderInfo },
        { "vnp_PayDate", request.PayDate },
        { "vnp_ResponseCode", request.VnPayResponseCode },
        { "vnp_TmnCode", request.TmnCode },
        { "vnp_TransactionNo", request.TransactionNo },
        { "vnp_TransactionStatus", request.TransactionStatus },
        { "vnp_TxnRef", request.TxnRef },
        { "vnp_SecureHash", request.SecureHash }
    };

            foreach (var (key, value) in responseData)
            {
                vnpay.AddResponseData(key, value);
            }
            var txnRefString = vnpay.GetResponseData("vnp_TxnRef");

            if (string.IsNullOrEmpty(txnRefString))
            {
                return new VnPaymentResponseModel
                {
                    Success = false,
                    VnPayResponseCode = "InvalidTxnRef"
                };
            }
            if (!long.TryParse(txnRefString, out var vnp_orderId) ||
                !long.TryParse(vnpay.GetResponseData("vnp_TransactionNo"), out var vnp_TransactionId))
            {
                return new VnPaymentResponseModel
                {
                    Success = false,
                    VnPayResponseCode = "InvalidTransactionId"
                };
            }
            if (request.TransactionStatus != "00")
            {
                return new VnPaymentResponseModel
                {
                    Success = false,
                    VnPayResponseCode = "TransactionFailed"
                };
            }
            //var vnp_orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
            //var vnp_TransactionId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            var vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            var vnp_SecureHash = vnpay.GetResponseData("vnp_SecureHash");
            var vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo");

            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _config["VnPay:HashSecret"]);
            if (!checkSignature)
            {
                return new VnPaymentResponseModel
                {
                    Success = false
                };
            }
            return new VnPaymentResponseModel
            {
                Success = true,
                PaymentMethod = "VnPay",
                OrderDescription = vnp_OrderInfo,
                OrderId = (int)vnp_orderId,
                TransactionId = vnp_TransactionId.ToString(),
                Token = vnp_SecureHash,
                VnPayResponseCode = vnp_ResponseCode.ToString()
            };
        }
    }
}
