using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.MembershipDTOs
{
    public class GetMembershipDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Types? Type { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
