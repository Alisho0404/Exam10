using Domain.DTOs.MembershipDTOs;
using Domain.Filters;
using Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.MembershipService
{
    public interface IMembershipService
    {
        Task<Response<List<GetMembershipDto>>> GetMembershipsAsync(PaginationFilter filter);
        Task<Response<GetMembershipDto>> GetMembershipByIdAsync(int id);
        Task<Response<string>> AddMembershipAsync(CreateMembershipDto membership);
        Task<Response<string>> UpdateMembershipAsync(UpdateMembershipDto membership);
        Task<Response<bool>> DeleteMembershipAsync(int id);
    }
}
