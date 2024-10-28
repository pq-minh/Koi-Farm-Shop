using KoiShop.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KoiShop.Controllers
{
    [ApiController]
    [Route("api/quotation")]
    public class QuotationManagementController : Controller
    {
        private readonly IQuotationService _quotationService;

        public QuotationManagementController(IQuotationService quotationService)
        {
            _quotationService = quotationService;
        }

        [HttpGet("list")] 
        public async Task<IActionResult> GetQuotations([FromQuery] string status, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var quotations = await _quotationService.GetQuotations(status, startDate, endDate);
                return Ok(quotations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"There is an error: {ex.Message}");
            }
        }

        [HttpGet("most-consigned-koi")] 
        public async Task<IActionResult> GetMostConsignedKoi([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                int koiId = await _quotationService.GetMostConsignedKoi(startDate, endDate);
                return Ok(koiId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"There is an error: {ex.Message}");
            }
        }
    }
}
