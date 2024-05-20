using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.WorkoutDTOs
{
    public class UpdateWorkoutDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Decription { get; set; }
        public int Duration { get; set; }
        public Intensity Intensity { get; set; }
    }
}
