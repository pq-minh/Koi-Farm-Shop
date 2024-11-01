using AutoMapper;
using KoiShop.Application.Dtos;
using KoiShop.Application.Interfaces;
using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KoiShop.Application.Users.UserContext;

namespace KoiShop.Application.Service
{
    public class RequestCareService : IRequestCareService
    {
        private readonly IMapper _mapper;
        private readonly IRequestCareRepository _requestCareRepository;
        private readonly IUserStore<User> _userStore;
        private readonly IUserContext _userContext;

        public RequestCareService(IMapper mapper, IRequestCareRepository requestCareRepository, IUserContext userContext, IUserStore<User> userStore)
        {
            _mapper = mapper;
            _userContext = userContext;
            _userStore = userStore;
            _requestCareRepository = requestCareRepository;
        }

        public async Task<IEnumerable<OrderDetailDtos>> GetCurrentOrderdetail()
        {
            if (_userContext.GetCurrentUser() == null || _userStore == null)
            {
                throw new ArgumentException("User context or user store is not valid.");
            }
            var userId = _userContext.GetCurrentUser().Id;
            if (userId == null)
            {
                return Enumerable.Empty<OrderDetailDtos>();
            }
            var orderDetail = await _requestCareRepository.GetCurrentOrderdetail();
            if (orderDetail == null)
            {
                return Enumerable.Empty<OrderDetailDtos>();
            }
            var orderDetailDto = _mapper.Map<IEnumerable<OrderDetailDtos>>(orderDetail);
            return orderDetailDto;
        }
        public async Task<IEnumerable<OrderDetailDtos>> GetAllOrderDetail()
        {
            if (_userContext.GetCurrentUser() == null || _userStore == null)
            {
                throw new ArgumentException("User context or user store is not valid.");
            }
            var userId = _userContext.GetCurrentUser().Id;
            if (userId == null)
            {
                return Enumerable.Empty<OrderDetailDtos>();
            }
            var orderDetail = await _requestCareRepository.GetAllOrderDetail();
            if (orderDetail == null)
            {
                return Enumerable.Empty<OrderDetailDtos>();
            }
            var orderDetailDto = _mapper.Map<IEnumerable<OrderDetailDtos>>(orderDetail);
            if (orderDetailDto == null)
            {
                return Enumerable.Empty<OrderDetailDtos>();
            }
            return orderDetailDto;
        }

        public async Task<IEnumerable<RequestCareDtos>> GetAllRequestCareByCustomer()
        {
            if (_userContext.GetCurrentUser() == null || _userStore == null)
            {
                throw new ArgumentException("User context or user store is not valid.");
            }
            var userId = _userContext.GetCurrentUser().Id;
            if (userId == null)
            {
                return Enumerable.Empty<RequestCareDtos>();
            }
            var request = await _requestCareRepository.GetAllRequestCareByCustomer();
            if (request == null) { return Enumerable.Empty<RequestCareDtos>(); }
            var requestDto = _mapper.Map<IEnumerable<RequestCareDtos>>(request);
            if (requestDto == null) { return Enumerable.Empty<RequestCareDtos>(); }
            return requestDto;
        }
        public async Task<IEnumerable<RequestCareDtos>> GetAllRequestCareByStaff()
        {
            if (_userContext.GetCurrentUser() == null || _userStore == null)
            {
                throw new ArgumentException("User context or user store is not valid.");
            }
            var userId = _userContext.GetCurrentUser().Id;
            if (userId == null)
            {
                return Enumerable.Empty<RequestCareDtos>();
            }
            var request = await _requestCareRepository.GetAllRequestCareByStaff();
            if (request == null) { return Enumerable.Empty<RequestCareDtos>(); }
            var requestDto = _mapper.Map<IEnumerable<RequestCareDtos>>(request);
            if (requestDto == null) { return Enumerable.Empty<RequestCareDtos>(); }
            return requestDto;
        }
        public async Task<bool> AddKoiOrBatchToPackage(List<OrderDetailDtoV1> orderDetails)
        {
            if (_userContext.GetCurrentUser() == null || _userStore == null)
            {
                throw new ArgumentException("User context or user store is not valid.");
            }
            var userId = _userContext.GetCurrentUser().Id;
            if (userId == null || orderDetails == null)
            {
                return false;
            }
            var orderDetail = _mapper.Map<List<OrderDetail>>(orderDetails);
            var result = await _requestCareRepository.AddKoiOrBatchToPackage(orderDetail);
            if (result)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> AddKoiOrBatchToRequest(List<OrderDetailDtoV1> orderDetails, DateTime endDate)
        {
            if (_userContext.GetCurrentUser() == null || _userStore == null)
            {
                throw new ArgumentException("User context or user store is not valid.");
            }
            var userId = _userContext.GetCurrentUser().Id;
            if (userId == null || orderDetails == null)
            {
                return false;
            }
            var orderDetail = _mapper.Map<List<OrderDetail>>(orderDetails);
            var result = await _requestCareRepository.AddKoiOrBatchToRequest(orderDetail, endDate);
            if (result)
            {
                return true;
            }
            return false;
        }
        public async Task< RequestCareEnum> AddFullKoiOrBatchToRequest(List<OrderDetailDtoV1> orderDetails, DateTime endDate)
        {
            if (_userContext.GetCurrentUser() == null || _userStore == null)
            {
                return RequestCareEnum.UserNotAuthenticated;
            }
            var userId = _userContext.GetCurrentUser().Id;
            if (userId == null || orderDetails == null)
            {
                return RequestCareEnum.UserNotAuthenticated;
            }
            var orderDetail = _mapper.Map<List<OrderDetail>>(orderDetails);
            var result = await _requestCareRepository.AddKoiOrBatchToPackage(orderDetail);
            if (result)
            {
                var request = await _requestCareRepository.AddKoiOrBatchToRequest(orderDetail, endDate);
                if (request)
                {
                    return RequestCareEnum.Success;
                }
                return RequestCareEnum.FailAddRequest;
            }
            return RequestCareEnum.Fail;
        }
        public async Task<bool> UpdateKoiOrBatchToCare(int? id, int? status)
        {
            if (_userContext.GetCurrentUser() == null || _userStore == null)
            {
                throw new ArgumentException("User context or user store is not valid.");
            }
            var userId = _userContext.GetCurrentUser().Id;
            if (userId == null || !id.HasValue || id < 0 || status < 0 || status == null || status > 3 )
            {
                return false;
            }
            string? reviewStatus = null;
            if (status == 1)
            {
                reviewStatus = "UnderCare";
            }
            else if (status == 2)
            {
                reviewStatus = "CompletedCare";
            }
            else if (status == 3)
            {
                reviewStatus = "RefusedCare";
            }
            if (reviewStatus == "UnderCare")
            {
                var result = await _requestCareRepository.UpdateKoiOrBatchToCare(id, reviewStatus);
                if (result)
                {
                    return true;
                }
                return false;
            }
            var validation = await _requestCareRepository.CheckRequest(id, reviewStatus);
            if (validation)
            {
                var result = await _requestCareRepository.UpdateKoiOrBatchToCare(id, reviewStatus);
                if (result)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
