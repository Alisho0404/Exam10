using Domain.Enteties;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seed
{
    public class Seeder
    {
        private readonly DataContext context;
        public Seeder(DataContext _context)
        {
            context = _context;
        } 
        public async Task SeedAdminUser()
        {
            var existing = await context.UserAdmin.FirstOrDefaultAsync
                (x => x.Name == "admin" && x.HashPassword == ConvertToHash("0404")); 
            if (existing != null)
            {
                return;
            }
            var admin = new UserAdmin()
            {
                Id = 1,
                Name = "admin",
                PhoneNumber = "909662643",
                CreatedAt = DateTime.UtcNow,
                HashPassword = ConvertToHash("0404"),
            };

            admin.Email = $"{admin.Name}@gmail.com";
            await context.UserAdmin.AddAsync(admin);
            await context.SaveChangesAsync();            
        }
        private static string ConvertToHash(string rawData)
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
