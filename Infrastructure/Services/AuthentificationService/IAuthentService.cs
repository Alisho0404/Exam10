using Domain.DTOs.AuthentificationDto;
using Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.AuthentificationService
{
    public interface IAuthentService
    {
        public Task<Response<string>> Login(LoginDto loginDto);
    }
}
