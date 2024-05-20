using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.PaymentDTOs
{
    public class CreatePaymentDto
    {
       
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; }
        public Status? Status { get; set; }
    }
}
