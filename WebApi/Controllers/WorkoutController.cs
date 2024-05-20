using Domain.DTOs.WorkoutDTOs;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.WorkoutService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class WorkoutController(IWorkoutService WorkoutService) : ControllerBase
    {
        [HttpGet]
        public async Task<Response<List<GetWorkoutDto>>> GetWorkoutsAsync([FromQuery] PaginationFilter WorkoutFilter)
            => await WorkoutService.GetWorkoutsAsync(WorkoutFilter);

        [HttpGet("{WorkoutId:int}")]
        public async Task<Response<GetWorkoutDto>> GetWorkoutByIdAsync(int WorkoutId)
            => await WorkoutService.GetWorkoutByIdAsync(WorkoutId);

        [HttpPost("create")]
        public async Task<Response<string>> CreateWorkoutAsync([FromBody] CreateWorkoutDto Workout)
            => await WorkoutService.AddWorkoutAsync(Workout);


        [HttpPut("update")]
        public async Task<Response<string>> UpdateWorkoutAsync([FromBody] UpdateWorkoutDto Workout)
            => await WorkoutService.UpdateWorkoutAsync(Workout);

        [HttpDelete("{WorkoutId:int}")]
        public async Task<Response<bool>> DeleteWorkoutAsync(int WorkoutId)
            => await WorkoutService.DeleteWorkoutAsync(WorkoutId);
    }
}
