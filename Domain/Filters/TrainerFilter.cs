using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Filters
{
    public class TrainerFilter:PaginationFilter
    {
        public string? Name { get; set; }
    }
}
