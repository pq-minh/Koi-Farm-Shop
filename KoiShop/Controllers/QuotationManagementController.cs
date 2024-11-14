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

        [HttpGet("get")]
        public async Task<IActionResult> GetQuotations([FromQuery] string status, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var quotations = await _quotationService.GetQuotations(status, startDate, endDate);
            if (quotations == null) return NotFound();
            return Ok(quotations);
        }

        [HttpGet("most-consigned-koi")]
        public async Task<IActionResult> GetMostConsignedKoi([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            int koiId = await _quotationService.GetMostConsignedKoi(startDate, endDate);
            if (koiId == -1) return NotFound();
            return Ok(koiId);
        }
    }
}
