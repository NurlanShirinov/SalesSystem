using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.Core.Enums;
using SalesSystem.Core.RequestModels;
using SalesSystem.Service.Services.Abstract;
using System.Security.Claims;

namespace SalesSystem.Api.Controllers
{
    [Authorize(Roles = nameof(UserType.Admin) + "," + nameof(UserType.Cashier))]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IHttpContextAccessor _contextAccessor;

        public InvoiceController(IInvoiceService invoiceService, IHttpContextAccessor contextAccessor)
        {
            _invoiceService = invoiceService;
            _contextAccessor = contextAccessor;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostInvoiceRequestModel model)
        {
            var cashierId = (_contextAccessor.HttpContext!.User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier))!.Value;

            model.Invoice.CashierId = new Guid(cashierId);

            var result = await _invoiceService.Post(model);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PutInvoiceRequestModel model)
        {
            var result = await _invoiceService.Put(model);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _invoiceService.Delete(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            var res = await _invoiceService.GetById(id);
            return Ok(res);
        }
    }
}
