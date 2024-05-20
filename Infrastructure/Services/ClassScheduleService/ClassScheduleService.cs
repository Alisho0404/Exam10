using Domain.DTOs.ClassScheduleDTOs;
using Domain.Enteties;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Infrastructure.Services.ClassScheduleService
{
    public class ClassScheduleService : IClassScheduleService
    {
        private readonly DataContext context;
        public ClassScheduleService(DataContext _context)
        {
            context = _context;
        }
        public async Task<Response<string>> AddClassScheduleAsync(CreateClassScheduleDto classSchedule)
        {
            try
            {
                var newClassSchedule = new ClassSchedule()
                {
                    WorkoutId=classSchedule.WorkoutId, 
                    TrainerId=classSchedule.TrainerId,
                    DateTime=classSchedule.DateTime,
                    Duration=classSchedule.Duration,
                    Location=classSchedule.Location
                   

    };
                await context.ClassSchedules.AddAsync(newClassSchedule);
                await context.SaveChangesAsync();

                return new Response<string>(HttpStatusCode.OK, "Successfully added ClassSchedule");
            }
            catch (Exception e)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<bool>> DeleteClassScheduleAsync(int id)
        {
            try
            {
                var classSchedule = await context.ClassSchedules.FirstOrDefaultAsync(x => x.Id == id);
                if (classSchedule == null) return new Response<bool>(HttpStatusCode.NotFound, "ClassSchedule not found");

                context.ClassSchedules.Remove(classSchedule);
                await context.SaveChangesAsync();

                return new Response<bool>(true);
            }
            catch (Exception e)
            {
                return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<GetClassScheduleDto>> GetClassScheduleByIdAsync(int id)
        {
            try
            {
                var classSchedule = await context.ClassSchedules.FirstOrDefaultAsync(x => x.Id == id);
                if (classSchedule == null) return new Response<GetClassScheduleDto>(HttpStatusCode.NotFound, "ClassSchedule not found");
                var result = new GetClassScheduleDto()
                { 
                    Id=classSchedule.Id,
                    WorkoutId = classSchedule.WorkoutId,
                    TrainerId = classSchedule.TrainerId,
                    DateTime = classSchedule.DateTime,
                    Duration = classSchedule.Duration,
                    Location = classSchedule.Location
                };

                return new Response<GetClassScheduleDto>(result);
            }
            catch (Exception e)
            {
                return new Response<GetClassScheduleDto>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<List<GetClassScheduleDto>>> GetClassSchedulesAsync(PaginationFilter filter)
        {
            try
            {
                var query = context.ClassSchedules.AsQueryable();

               

                var totalRecord = await query.CountAsync();

                var ClassSchedules = await query.Select(x => new GetClassScheduleDto()
                {
                    Id = x.Id,
                    WorkoutId = x.WorkoutId,
                    TrainerId = x.TrainerId,
                    DateTime = x.DateTime,
                    Duration = x.Duration,
                    Location = x.Location

                }).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                    .ToListAsync();

                return new PagedResponse<List<GetClassScheduleDto>>(ClassSchedules, totalRecord, filter.PageNumber, filter.PageSize);
            }
            catch (Exception e)
            {
                return new Response<List<GetClassScheduleDto>>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<string>> UpdateClassScheduleAsync(UpdateClassScheduleDto classSchedule)
        {
            try
            {
                var request = await context.ClassSchedules.FirstOrDefaultAsync(x => x.Id == classSchedule.Id);
                if (request == null) return new Response<string>(HttpStatusCode.NotFound, "ClassSchedule not found");

                request.Id = classSchedule.Id;
                request.WorkoutId = classSchedule.WorkoutId;
                request.TrainerId = classSchedule.TrainerId;
                request.DateTime = classSchedule.DateTime;
                request.Duration= classSchedule.Duration;
                request.Location = classSchedule.Location;                

                await context.SaveChangesAsync();

                return new Response<string>(HttpStatusCode.OK, "ClassSchedule updated successfully");
            }
            catch (Exception e)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
