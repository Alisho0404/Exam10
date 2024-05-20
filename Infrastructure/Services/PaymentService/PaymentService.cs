using Domain.DTOs.PaymentDTOs;
using Domain.Enteties;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Infrastructure.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly DataContext context;
        public PaymentService(DataContext _context)
        {
            context = _context;
        }
        public async Task<Response<string>> AddPaymentAsync(CreatePaymentDto payment)
        {
            try
            {
                var newPayment = new Payment()
                {
                    UserId = payment.UserId,
                    Amount = payment.Amount,
                    DateTime = payment.DateTime,
                    Status = payment.Status

                };
                await context.Payments.AddAsync(newPayment);
                await context.SaveChangesAsync();

                return new Response<string>(HttpStatusCode.OK, "Successfully added Payment");
            }
            catch (Exception e)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<bool>> DeletePaymentAsync(int id)
        {
            try
            {
                var payment = await context.Payments.FirstOrDefaultAsync(x => x.Id == id);
                if (payment == null) return new Response<bool>(HttpStatusCode.NotFound, "Payment not found");

                context.Payments.Remove(payment);
                await context.SaveChangesAsync();

                return new Response<bool>(true);
            }
            catch (Exception e)
            {
                return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<GetPaymentDto>> GetPaymentByIdAsync(int id)
        {
            try
            {
                var payment = await context.Payments.FirstOrDefaultAsync(x => x.Id == id);
                if (payment == null) return new Response<GetPaymentDto>(HttpStatusCode.NotFound, "Payment not found");
                var result = new GetPaymentDto()
                {
                    Id = payment.Id,
                    UserId = payment.UserId,
                    Amount = payment.Amount,
                    DateTime = payment.DateTime,
                    Status = payment.Status
                };

                return new Response<GetPaymentDto>(result);
            }
            catch (Exception e)
            {
                return new Response<GetPaymentDto>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<List<GetPaymentDto>>> GetPaymentsAsync(PaginationFilter filter)
        {
            try
            {
                var query = context.Payments.AsQueryable();


                var totalRecord = await query.CountAsync();

                var Payments = await query.Select(x => new GetPaymentDto()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Amount = x.Amount,
                    DateTime = x.DateTime,
                    Status = x.Status

                }).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                    .ToListAsync();

                return new PagedResponse<List<GetPaymentDto>>(Payments, totalRecord, filter.PageNumber, filter.PageSize);
            }
            catch (Exception e)
            {
                return new Response<List<GetPaymentDto>>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<string>> UpdatePaymentAsync(UpdatePaymentDto payment)
        {
            try
            {
                var request = await context.Payments.FirstOrDefaultAsync(x => x.Id == payment.Id);
                if (request == null) return new Response<string>(HttpStatusCode.NotFound, "Payment not found");

                request.Id = payment.Id;
                request.UserId = payment.UserId;
                request.Amount = payment.Amount;
                request.DateTime = payment.DateTime;
                request.Status = payment.Status;

                await context.SaveChangesAsync();

                return new Response<string>(HttpStatusCode.OK, "Payment updated successfully");
            }
            catch (Exception e)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
