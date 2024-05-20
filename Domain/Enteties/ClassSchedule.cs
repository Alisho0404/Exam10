using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enteties
{
    public class ClassSchedule
    {
        public int Id { get; set; }
        public int WorkoutId { get; set; }
        public int TrainerId { get; set; }
        public DateTime DateTime { get; set; }
        public int Duration { get; set; }
        public string? Location { get; set; }
        public Trainer? Trainer { get; set; }
        public Workout? Workout { get; set; }


    }
}
