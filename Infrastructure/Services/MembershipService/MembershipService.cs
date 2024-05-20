using Domain.DTOs.MembershipDTOs;
using Domain.Enteties;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Infrastructure.Services.MembershipService
{
    public class MembershipService : IMembershipService
    {
        private readonly DataContext context;
        public MembershipService(DataContext _context)
        {
            context = _context;
        }
        public async Task<Response<string>> AddMembershipAsync(CreateMembershipDto membership)
        {
            try
            {
                var newMembership = new Membership()
                { 
                    UserId=membership.UserId,
                    Type=membership.Type,
                    Price=membership.Price,
                    StartDate=membership.StartDate,
                    EndDate=membership.EndDate
                    

    };
                await context.Memberships.AddAsync(newMembership);
                await context.SaveChangesAsync();

                return new Response<string>(HttpStatusCode.OK, "Successfully added Membership");
            }
            catch (Exception e)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<bool>> DeleteMembershipAsync(int id)
        {
            try
            {
                var membership = await context.Memberships.FirstOrDefaultAsync(x => x.Id == id);
                if (membership == null) return new Response<bool>(HttpStatusCode.NotFound, "Membership not found");

                context.Memberships.Remove(membership);
                await context.SaveChangesAsync();

                return new Response<bool>(true);
            }
            catch (Exception e)
            {
                return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<GetMembershipDto>> GetMembershipByIdAsync(int id)
        {
            try
            {
                var membership = await context.Memberships.FirstOrDefaultAsync(x => x.Id == id);
                if (membership == null) return new Response<GetMembershipDto>(HttpStatusCode.NotFound, "Membership not found");
                var result = new GetMembershipDto()
                { 
                    Id=membership.Id,
                    UserId = membership.UserId,
                    Type = membership.Type,
                    Price = membership.Price,
                    StartDate = membership.StartDate,
                    EndDate = membership.EndDate
                };

                return new Response<GetMembershipDto>(result);
            }
            catch (Exception e)
            {
                return new Response<GetMembershipDto>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<List<GetMembershipDto>>> GetMembershipsAsync(PaginationFilter filter)
        {
            try
            {
                var query = context.Memberships.AsQueryable();               

                var totalRecord = await query.CountAsync();

                var memberships = await query.Select(x => new GetMembershipDto()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Type = x.Type,
                    Price = x.Price,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate

                }).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                    .ToListAsync();

                return new PagedResponse<List<GetMembershipDto>>(memberships, totalRecord, filter.PageNumber, filter.PageSize);
            }
            catch (Exception e)
            {
                return new Response<List<GetMembershipDto>>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<string>> UpdateMembershipAsync(UpdateMembershipDto membership)
        {
            try
            {
                var request = await context.Memberships.FirstOrDefaultAsync(x => x.Id == membership.Id);
                if (request == null) return new Response<string>(HttpStatusCode.NotFound, "Membership not found");

                request.Id = membership.Id;
                request.UserId = membership.UserId;
                request.Type = membership.Type;
                request.Price = membership.Price;
                request.StartDate = membership.StartDate;
                request.EndDate = membership.EndDate;
                

                await context.SaveChangesAsync();

                return new Response<string>(HttpStatusCode.OK, "Membership updated successfully");
            }
            catch (Exception e)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
