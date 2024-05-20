using Domain.DTOs.AuthentificationDto;
using Domain.Enteties;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services.AuthentificationService
{
    internal class AuthentService(DataContext context, IConfiguration configuration) : IAuthentService
    {
        
        public async Task<Response<string>> Login(LoginDto loginDto)
        {
            var admin = await context.UserAdmin.FirstOrDefaultAsync(x =>
             x.Name == loginDto.UserName && x.HashPassword == ConvertToHash(loginDto.Password));
            if (admin == null) return new Response<string>(HttpStatusCode.BadRequest, "Incorrect name or password");

            return new Response<string>(GenerateJwtToken(admin));
        }

        private string GenerateJwtToken(UserAdmin user)
        {
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!);
            var securityKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Name, user.Name),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Sub, user.PhoneNumber),
        };
            //add roles

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            var securityTokenHandler = new JwtSecurityTokenHandler();
            var tokenString = securityTokenHandler.WriteToken(token);
            return tokenString;
        }

        private string ConvertToHash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
