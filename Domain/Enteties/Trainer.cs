using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enteties
{
    public class Trainer
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public  string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Specialization { get; set; }
        public string? Photo { get; set; }
        

    }
}
