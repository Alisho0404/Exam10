using Domain.DTOs.UserDTOs;
using Domain.Filters;
using Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.UserService
{
    public interface IUserService
    {
        Task<Response<List<GetUserDto>>> GetUsersAsync(UserFilter filter);
        Task<Response<GetUserDto>> GetUserByIdAsync(int id);
        Task<Response<string>> AddUserAsync(CreateUserDto user);
        Task<Response<string>> UpdateUserAsync(UpdateUserDto user);
        Task<Response<bool>> DeleteUserAsync(int id);
    }
}
