using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClassScheduleDTOs
{
    public class UpdateClassScheduleDto
    {
        public int Id { get; set; }
        public int WorkoutId { get; set; }
        public int TrainerId { get; set; }
        public DateTime DateTime { get; set; }
        public int Duration { get; set; }
        public string? Location { get; set; }
    }
}
