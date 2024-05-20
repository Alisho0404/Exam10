using Domain.DTOs.PaymentDTOs;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.PaymentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PaymentController(IPaymentService PaymentService) : ControllerBase
    {
        [HttpGet]
        public async Task<Response<List<GetPaymentDto>>> GetPaymentsAsync([FromQuery] PaginationFilter PaymentFilter)
            => await PaymentService.GetPaymentsAsync(PaymentFilter);

        [HttpGet("{PaymentId:int}")]
        public async Task<Response<GetPaymentDto>> GetPaymentByIdAsync(int PaymentId)
            => await PaymentService.GetPaymentByIdAsync(PaymentId);

        [HttpPost("create")]
        public async Task<Response<string>> CreatePaymentAsync([FromBody] CreatePaymentDto Payment)
            => await PaymentService.AddPaymentAsync(Payment);


        [HttpPut("update")]
        public async Task<Response<string>> UpdatePaymentAsync([FromBody] UpdatePaymentDto Payment)
            => await PaymentService.UpdatePaymentAsync(Payment);

        [HttpDelete("{PaymentId:int}")]
        public async Task<Response<bool>> DeletePaymentAsync(int PaymentId)
            => await PaymentService.DeletePaymentAsync(PaymentId);
    }
}
