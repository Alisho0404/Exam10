using Domain.DTOs.TrainerDTOs;
using Domain.Enteties;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Infrastructure.Services.TrainerService
{
    public class TrainerService:ITrainerService
    {
        private readonly DataContext context;
        public TrainerService(DataContext _context)
        {
            context = _context;
        }

        public async Task<Response<string>> AddTrainerAsync(CreateTrainerDto trainer)
        {
            try
            {
                var newTrainer = new Trainer()
                { 
                    Name=trainer.Name,
                    Email=trainer.Email,
                    Phone=trainer.Phone,
                    Specialization=trainer.Specialization,
                    Photo=trainer.Photo
                    

    };
                await context.Trainers.AddAsync(newTrainer);
                await context.SaveChangesAsync();

                return new Response<string>(HttpStatusCode.OK, "Successfully added Trainer");
            }
            catch (Exception e)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<bool>> DeleteTrainerAsync(int id)
        {
            try
            {
                var trainer = await context.Trainers.FirstOrDefaultAsync(x => x.Id == id);
                if (trainer == null) return new Response<bool>(HttpStatusCode.NotFound, "Trainer not found");

                context.Trainers.Remove(trainer);
                await context.SaveChangesAsync();

                return new Response<bool>(true);
            }
            catch (Exception e)
            {
                return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<GetTrainerDto>> GetTrainerByIdAsync(int id)
        {
            try
            {
                var trainer = await context.Trainers.FirstOrDefaultAsync(x => x.Id == id);
                if (trainer == null) return new Response<GetTrainerDto>(HttpStatusCode.NotFound, "Trainer not found");
                var result = new GetTrainerDto()
                { 
                    Id=trainer.Id,
                    Name = trainer.Name,
                    Email = trainer.Email,
                    Phone = trainer.Phone,
                    Specialization = trainer.Specialization,
                    Photo = trainer.Photo
                };

                return new Response<GetTrainerDto>(result);
            }
            catch (Exception e)
            {
                return new Response<GetTrainerDto>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<List<GetTrainerDto>>> GetTrainersAsync(TrainerFilter filter)
        {
            try
            {
                var query = context.Trainers.AsQueryable();

                if (!string.IsNullOrEmpty(filter.Name))
                    query = query.Where(x =>
                        x.Name!.ToLower().Contains(filter.Name.ToLower()));

                var totalRecord = await query.CountAsync();

                var Trainers = await query.Select(x => new GetTrainerDto()
                { 
                    Id=x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Phone = x.Phone,
                    Specialization = x.Specialization,
                    Photo = x.Photo

                }).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                    .ToListAsync();

                return new PagedResponse<List<GetTrainerDto>>(Trainers, totalRecord, filter.PageNumber, filter.PageSize);
            }
            catch (Exception e)
            {
                return new Response<List<GetTrainerDto>>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<string>> UpdateTrainerAsync(UpdateTrainerDto trainer)
        {
            try
            {
                var request = await context.Trainers.FirstOrDefaultAsync(x => x.Id == trainer.Id);
                if (request == null) return new Response<string>(HttpStatusCode.NotFound, "Trainer not found");

                request.Id = trainer.Id;
                request.Name = trainer.Name;
                request.Email = trainer.Email;
                request.Phone = trainer.Phone;
                request.Specialization = trainer.Specialization;
                request.Photo = trainer.Photo;

                await context.SaveChangesAsync();

                return new Response<string>(HttpStatusCode.OK, "Trainer updated successfully");
            }
            catch (Exception e)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
