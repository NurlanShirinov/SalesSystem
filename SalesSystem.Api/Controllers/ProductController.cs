using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.Core.Enums;
using SalesSystem.Core.Models;
using SalesSystem.Core.RequestModels;
using SalesSystem.Service.Services.Abstract;

namespace SalesSystem.Api.Controllers
{
    [Authorize(Roles = nameof(UserType.Admin))]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _productService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("GetByNameAsync")]
        public async Task<IActionResult> GetByName(string name)
        {
            var result = await _productService.GetByNameAsync(name);
            return Ok(result);
        }

        [HttpPost("Post")]
        public async Task<IActionResult> Post([FromBody] ProductRequestModel model)
        {
            var result = await _productService.Post(model);
            return Ok(result);
        }

        [HttpPut("Put")]
        public async Task<IActionResult> Put([FromBody] Product product)
        {
            await _productService.Put(product);
            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _productService.Delete(id);
            return Ok(result);
        }

        [HttpPut("ApplyDiscount")]
        public async Task<IActionResult> ApplyDiscount([FromBody] ApplyDiscountRequestModel model)
        {
            await _productService.ApplyDiscount(model);
            return Ok();    
        }

        [HttpGet("GetProductsByDiscount")]
        public async Task<IActionResult> GetProductsByDiscount(Guid discountId)
        {
            var result = await _productService.GetProductsByDiscount(discountId);
            return Ok(result);
        }

    }
}
