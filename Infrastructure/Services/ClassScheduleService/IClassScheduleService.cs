using Domain.DTOs.ClassScheduleDTOs;
using Domain.Filters;
using Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.ClassScheduleService
{
    public interface IClassScheduleService
    {
        Task<Response<List<GetClassScheduleDto>>> GetClassSchedulesAsync(PaginationFilter filter);
        Task<Response<GetClassScheduleDto>> GetClassScheduleByIdAsync(int id);
        Task<Response<string>> AddClassScheduleAsync(CreateClassScheduleDto classSchedule);
        Task<Response<string>> UpdateClassScheduleAsync(UpdateClassScheduleDto classSchedule);
        Task<Response<bool>> DeleteClassScheduleAsync(int id);
    }
}
