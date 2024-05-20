using Domain.DTOs.UserDTOs;
using Domain.Enteties;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Infrastructure.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext context;
        public UserService(DataContext _context)
        {
            context = _context;
        }

        public async Task<Response<string>> AddUserAsync(CreateUserDto user)
        {
            try
            {
                var newUser = new User()
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    RegistrationDate = user.RegistrationDate,
                    Role = user.Role,
                    MembershipId = user.MembershipId

                };
                await context.Users.AddAsync(newUser);
                await context.SaveChangesAsync();

                return new Response<string>(HttpStatusCode.OK, "Successfully added User");
            }
            catch (Exception e)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<bool>> DeleteUserAsync(int id)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user == null) return new Response<bool>(HttpStatusCode.NotFound, "User not found");

                context.Users.Remove(user);
                await context.SaveChangesAsync();

                return new Response<bool>(true);
            }
            catch (Exception e)
            {
                return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<GetUserDto>> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user == null) return new Response<GetUserDto>(HttpStatusCode.NotFound, "user not found");
                var result = new GetUserDto()
                { 
                    Id= user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    RegistrationDate = user.RegistrationDate,
                    Role = user.Role,
                    MembershipId = user.MembershipId
                };

                return new Response<GetUserDto>(result);
            }
            catch (Exception e)
            {
                return new Response<GetUserDto>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<List<GetUserDto>>> GetUsersAsync(UserFilter filter)
        {
            try
            {
                var query = context.Users.AsQueryable();

                if (!string.IsNullOrEmpty(filter.Name))
                    query = query.Where(x =>
                        x.Name!.ToLower().Contains(filter.Name.ToLower()));

                var totalRecord = await query.CountAsync();

                var Users = await query.Select(x => new GetUserDto()
                { 
                    Id=x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Password = x.Password,
                    RegistrationDate = x.RegistrationDate,
                    Role = x.Role,
                    MembershipId = x.MembershipId

                }).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                    .ToListAsync();

                return new PagedResponse<List<GetUserDto>>(Users, totalRecord, filter.PageNumber, filter.PageSize);
            }
            catch (Exception e)
            {
                return new Response<List<GetUserDto>>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<string>> UpdateUserAsync(UpdateUserDto user)
        {
            try
            {
                var request = await context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
                if (request == null) return new Response<string>(HttpStatusCode.NotFound, "User not found");

                request.Id= user.Id;
                request.Name = user.Name;
                request.Email = user.Email;
                request.Password = user.Password;
                request.RegistrationDate = user.RegistrationDate;
                request.Role = user.Role;
                request.MembershipId = user.MembershipId;

                await context.SaveChangesAsync();

                return new Response<string>(HttpStatusCode.OK, "User updated successfully");
            }
            catch (Exception e)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
