using Domain.DTOs.ClassScheduleDTOs;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.ClassScheduleService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ClassScheduleController(IClassScheduleService ClassScheduleService) : ControllerBase
    {
        [HttpGet]
        public async Task<Response<List<GetClassScheduleDto>>> GetClassSchedulesAsync([FromQuery] PaginationFilter ClassScheduleFilter)
            => await ClassScheduleService.GetClassSchedulesAsync(ClassScheduleFilter);

        [HttpGet("{ClassScheduleId:int}")]
        public async Task<Response<GetClassScheduleDto>> GetClassScheduleByIdAsync(int ClassScheduleId)
            => await ClassScheduleService.GetClassScheduleByIdAsync(ClassScheduleId);

        [HttpPost("create")]
        public async Task<Response<string>> CreateClassScheduleAsync([FromBody] CreateClassScheduleDto ClassSchedule)
            => await ClassScheduleService.AddClassScheduleAsync(ClassSchedule);


        [HttpPut("update")]
        public async Task<Response<string>> UpdateClassScheduleAsync([FromBody] UpdateClassScheduleDto ClassSchedule)
            => await ClassScheduleService.UpdateClassScheduleAsync(ClassSchedule);

        [HttpDelete("{ClassScheduleId:int}")]
        public async Task<Response<bool>> DeleteClassScheduleAsync(int ClassScheduleId)
            => await ClassScheduleService.DeleteClassScheduleAsync(ClassScheduleId);
    }
}
