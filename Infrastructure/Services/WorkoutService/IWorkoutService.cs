using Domain.DTOs.WorkoutDTOs;
using Domain.Filters;
using Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.WorkoutService
{
    public interface IWorkoutService
    {
        Task<Response<List<GetWorkoutDto>>> GetWorkoutsAsync(PaginationFilter filter);
        Task<Response<GetWorkoutDto>> GetWorkoutByIdAsync(int id);
        Task<Response<string>> AddWorkoutAsync(CreateWorkoutDto workout);
        Task<Response<string>> UpdateWorkoutAsync(UpdateWorkoutDto workout);
        Task<Response<bool>> DeleteWorkoutAsync(int id);
    }
}
