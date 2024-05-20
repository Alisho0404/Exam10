using Domain.DTOs.AuthentificationDto;
using Domain.Responses;
using Infrastructure.Services.AuthentificationService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthentService authService)
    {
        [HttpPost("Login")]
        public async Task<Response<string>> Login([FromBody] LoginDto loginDto)
        => await authService.Login(loginDto);


    }
}
