using Domain.DTOs.TrainerDTOs;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.TrainerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TrainerController(ITrainerService TrainerService) : ControllerBase
    {
        [HttpGet]
        public async Task<Response<List<GetTrainerDto>>> GetTrainersAsync([FromQuery] TrainerFilter TrainerFilter)
            => await TrainerService.GetTrainersAsync(TrainerFilter);

        [HttpGet("{TrainerId:int}")]
        public async Task<Response<GetTrainerDto>> GetTrainerByIdAsync(int TrainerId)
            => await TrainerService.GetTrainerByIdAsync(TrainerId);

        [HttpPost("create")]
        public async Task<Response<string>> CreateTrainerAsync([FromBody] CreateTrainerDto Trainer)
            => await TrainerService.AddTrainerAsync(Trainer);


        [HttpPut("update")]
        public async Task<Response<string>> UpdateTrainerAsync([FromBody] UpdateTrainerDto Trainer)
            => await TrainerService.UpdateTrainerAsync(Trainer);

        [HttpDelete("{TrainerId:int}")]
        public async Task<Response<bool>> DeleteTrainerAsync(int TrainerId)
            => await TrainerService.DeleteTrainerAsync(TrainerId);
    }
}
