using Domain.DTOs.TrainerDTOs;
using Domain.Filters;
using Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.TrainerService
{
    public interface ITrainerService
    {
        Task<Response<List<GetTrainerDto>>> GetTrainersAsync(TrainerFilter filter);
        Task<Response<GetTrainerDto>> GetTrainerByIdAsync(int id);
        Task<Response<string>> AddTrainerAsync(CreateTrainerDto trainer);
        Task<Response<string>> UpdateTrainerAsync(UpdateTrainerDto trainer);
        Task<Response<bool>> DeleteTrainerAsync(int id);
    }
}
