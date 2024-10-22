using AutoMapper;
using KoiShop.Application.Dtos;
using KoiShop.Domain.Respositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KoiShop.Application.Users.UserContext;
using AutoMapper;
using KoiShop.Domain.Entities;
using KoiShop.Application.Interfaces;


namespace KoiShop.Application.Service
{
    public class DiscountService : IDiscountService
    {
        private readonly IMapper _mapper;
        private readonly IDiscountRepository _discountRepository;
        private readonly IUserStore<User> _userStore;
        private readonly IUserContext _userContext;

        public DiscountService(IMapper mapper, IDiscountRepository discountRepository, IUserContext userContext, IUserStore<User> userStore)
        {
            _mapper = mapper;
            _discountRepository = discountRepository;
            _userContext = userContext;
            _userStore = userStore;
        }
        public async Task<IEnumerable<DiscountDto>> GetDiscount()
        {
            var discount = await _discountRepository.GetDiscount();
            var discountDto = _mapper.Map<IEnumerable<DiscountDto>>(discount);
            return discountDto;
        }
        public async Task<IEnumerable<DiscountDto>> GetDiscountForUser()
        {
            if (_userContext.GetCurrentUser() == null || _userStore == null)
            {
                throw new ArgumentException("User context or user store is not valid.");
            }
            var userId = _userContext.GetCurrentUser().Id;
            if (userId == null)
            {
                return Enumerable.Empty<DiscountDto>();
            }
            var discount = await _discountRepository.GetDiscountForUser();
            var discountDto = _mapper.Map<IEnumerable<DiscountDto>>(discount);
            return discountDto;
        }
        public async Task<DiscountDto> GetDiscountForUser(string? name)
        {
            if (_userContext.GetCurrentUser() == null || _userStore == null)
            {
                throw new ArgumentException("User context or user store is not valid.");
            }
            var userId = _userContext.GetCurrentUser().Id;
            if (userId == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            var discount = await _discountRepository.GetDiscountForUser(name);
            var discountDto = _mapper.Map<DiscountDto>(discount);
            return discountDto;

        }
    }
}
