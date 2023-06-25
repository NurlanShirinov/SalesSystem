using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.Core.Enums;
using SalesSystem.Core.Models;
using SalesSystem.Service.Services.Abstract;
using System.Diagnostics.CodeAnalysis;

namespace SalesSystem.Api.Controllers
{
    [Authorize(Roles = nameof(UserType.Admin))]
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _discountService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _discountService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost("Post")]
        public async Task<IActionResult> Post([FromBody] Discount discount)
        {
            var result = await _discountService.Post(discount);
            return Ok(result);
        }

        [HttpPut("Put")]
        public async Task<IActionResult> Put([FromBody] Discount discount)
        {
            var result = await _discountService.Put(discount);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _discountService.Delete(id);
            return Ok(result);
        }

    }
}
