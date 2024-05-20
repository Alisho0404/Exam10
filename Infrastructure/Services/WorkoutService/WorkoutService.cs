using Domain.DTOs.WorkoutDTOs;
using Domain.Enteties;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Infrastructure.Services.WorkoutService
{
    public class WorkoutService : IWorkoutService
    {
        private readonly DataContext context;
        public WorkoutService(DataContext _context)
        {
            context = _context;
        }
        public async Task<Response<string>> AddWorkoutAsync(CreateWorkoutDto workout)
        {
            try
            {
                var newWorkout = new Workout()
                { 
                    Title=workout.Title,
                    Decription=workout.Decription,
                    Duration=workout.Duration,
                    Intensity=workout.Intensity
                    

    };
                await context.Workouts.AddAsync(newWorkout);
                await context.SaveChangesAsync();

                return new Response<string>(HttpStatusCode.OK, "Successfully added Workout");
            }
            catch (Exception e)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<bool>> DeleteWorkoutAsync(int id)
        {
            try
            {
                var workout = await context.Workouts.FirstOrDefaultAsync(x => x.Id == id);
                if (workout == null) return new Response<bool>(HttpStatusCode.NotFound, "Workout not found");

                context.Workouts.Remove(workout);
                await context.SaveChangesAsync();

                return new Response<bool>(true);
            }
            catch (Exception e)
            {
                return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<GetWorkoutDto>> GetWorkoutByIdAsync(int id)
        {
            try
            {
                var workout = await context.Workouts.FirstOrDefaultAsync(x => x.Id == id);
                if (workout == null) return new Response<GetWorkoutDto>(HttpStatusCode.NotFound, "Workout not found");
                var result = new GetWorkoutDto()
                {
                    Id=workout.Id,
                    Title = workout.Title,
                    Decription = workout.Decription,
                    Duration = workout.Duration,
                    Intensity = workout.Intensity

                };

                return new Response<GetWorkoutDto>(result);
            }
            catch (Exception e)
            {
                return new Response<GetWorkoutDto>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<List<GetWorkoutDto>>> GetWorkoutsAsync(PaginationFilter filter)
        {
            try
            {
                var query = context.Workouts.AsQueryable();               

                var totalRecord = await query.CountAsync();

                var workouts = await query.Select(x => new GetWorkoutDto()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Decription = x.Decription,
                    Duration = x.Duration,
                    Intensity = x.Intensity

                }).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                    .ToListAsync();

                return new PagedResponse<List<GetWorkoutDto>>(workouts, totalRecord, filter.PageNumber, filter.PageSize);
            }
            catch (Exception e)
            {
                return new Response<List<GetWorkoutDto>>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<string>> UpdateWorkoutAsync(UpdateWorkoutDto workout)
        {
            try
            {
                var request = await context.Workouts.FirstOrDefaultAsync(x => x.Id == workout.Id);
                if (request == null) return new Response<string>(HttpStatusCode.NotFound, "Workout not found");

                request.Id = workout.Id;
                request.Title = workout.Title;
                request.Decription = workout.Decription;
                request.Duration = workout.Duration;
                request.Intensity = workout.Intensity;                

                await context.SaveChangesAsync();

                return new Response<string>(HttpStatusCode.OK, "Workout updated successfully");
            }
            catch (Exception e)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
