using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enteties
{
    public class Workout
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Decription { get; set; }
        public int Duration { get; set; }
        public Intensity Intensity { get; set; }

    }
}
