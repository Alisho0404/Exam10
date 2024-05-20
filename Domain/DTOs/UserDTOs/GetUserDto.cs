using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.UserDTOs
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public required string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public Role Role { get; set; }
        public int MembershipId { get; set; }
    }
}
