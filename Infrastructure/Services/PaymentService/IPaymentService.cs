using Domain.DTOs.PaymentDTOs;
using Domain.Filters;
using Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<Response<List<GetPaymentDto>>> GetPaymentsAsync(PaginationFilter filter);
        Task<Response<GetPaymentDto>> GetPaymentByIdAsync(int id);
        Task<Response<string>> AddPaymentAsync(CreatePaymentDto payment);
        Task<Response<string>> UpdatePaymentAsync(UpdatePaymentDto payment);
        Task<Response<bool>> DeletePaymentAsync(int id);
    }
}
