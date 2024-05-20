using Domain.DTOs.UserDTOs;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController(IUserService UserService) : ControllerBase
    {
        [HttpGet]
        public async Task<Response<List<GetUserDto>>> GetUsersAsync([FromQuery] UserFilter UserFilter)
            => await UserService.GetUsersAsync(UserFilter);

        [HttpGet("{UserId:int}")]
        public async Task<Response<GetUserDto>> GetUserByIdAsync(int UserId)
            => await UserService.GetUserByIdAsync(UserId);

        [HttpPost("create")]
        public async Task<Response<string>> CreateUserAsync([FromBody] CreateUserDto User)
            => await UserService.AddUserAsync(User);


        [HttpPut("update")]
        public async Task<Response<string>> UpdateUserAsync([FromBody] UpdateUserDto User)
            => await UserService.UpdateUserAsync(User);

        [HttpDelete("{UserId:int}")]
        public async Task<Response<bool>> DeleteUserAsync(int UserId)
            => await UserService.DeleteUserAsync(UserId);
    }
}
