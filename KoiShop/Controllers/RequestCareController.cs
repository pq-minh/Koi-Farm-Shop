﻿using KoiShop.Application.Dtos;
using KoiShop.Application.Interfaces;
using KoiShop.Application.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace KoiShop.Controllers
{
    [ApiController]
    [Route("api/requestcare")]
    public class RequestCareController : ControllerBase
    {
        private readonly IRequestCareService _requestCareService;
        public RequestCareController(IRequestCareService requestCareService)
        {
            _requestCareService = requestCareService;
        }
        [HttpGet("orderdetail")]
        public async Task<IActionResult> GetCurrentOrderDetail()
        {
            var KoiOrBatchRequest = await _requestCareService.GetCurrentOrderdetail();
            if (KoiOrBatchRequest != null)
            {
                return Ok(KoiOrBatchRequest);
            }
            return BadRequest();
        }
        [HttpGet("allorderdetail")]
        public async Task<IActionResult> GetAllOrderDetail()
        {
            var orderDetail = await _requestCareService.GetAllOrderDetail();
            return Ok(orderDetail);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRequestCareByCustomer()
        {
            var request = await _requestCareService.GetAllRequestCareByCustomer();
            if (request != null)
            {
                return Ok(request);
            }
            return BadRequest();
        }
        [HttpGet("allrequest")]
        public async Task<IActionResult> GetAllRequestCareByStaff()
        {
            var request = await _requestCareService.GetAllRequestCareByStaff();
            if (request != null)
            {
                return Ok(request);
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> AddFullKoiOrBatchToRequest([FromBody] RequestCareDtoV1 requestCareDtoV1)
        {
            var orderDetails = requestCareDtoV1.OrderDetails;
            var endDate = requestCareDtoV1.EndDate;
            var result = await _requestCareService.AddFullKoiOrBatchToRequest(orderDetails, endDate);
            switch (result)
            {
                case RequestCareEnum.Success:
                    return Created();
                case RequestCareEnum.UserNotAuthenticated:
                    return BadRequest("User not authenticated");
                case RequestCareEnum.FailAddRequest:
                    return BadRequest("An error occurred during the final processing step");
                case RequestCareEnum.Fail:
                    return BadRequest("An error occurred during the add process");
                default:
                    return BadRequest("Unexpected error");
            }
        }
        [HttpPost("package")]
        public async Task<IActionResult> AddKoiOrBatchToPackage([FromBody] List<OrderDetailDtoV1> orderDetails)
        {
            var result = await _requestCareService.AddKoiOrBatchToPackage(orderDetails);
            if (result)
            {
                return Created();
            }
            return BadRequest("There was an error while adding data");
        }
        [HttpPost("request")]
        public async Task<IActionResult> AddKoiOrBatchToRequest([FromBody] RequestCareDtoV1 requestCareDtoV1)
        {
            var orderDetails = requestCareDtoV1.OrderDetails;
            var endDate = requestCareDtoV1.EndDate;
            var result = await _requestCareService.AddKoiOrBatchToRequest(orderDetails, endDate);
            if (result)
            {
                return Created();
            }
            return BadRequest("There was an error while adding data");
        }
        [HttpPatch("request")]
        public async Task<IActionResult> UpdateKoiOrBatchToRequest([FromBody] RequestCareDtoV2 requestCareDtoV2)
        {
            var id = requestCareDtoV2.Id;
            var status = requestCareDtoV2.Status;
            var result = await _requestCareService.UpdateKoiOrBatchToCare(id, status);
            if (result)
            {
                return Created();
            }
            return BadRequest("There was an error while adding data");
        }
    }
}
