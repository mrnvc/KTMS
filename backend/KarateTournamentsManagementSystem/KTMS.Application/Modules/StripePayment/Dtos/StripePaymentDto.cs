using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Modules.StripePayment.Dtos
{
    public class StripePaymentDto
    {
        public required string ClientSecret { get; set; }
        public required string Id { get; set; }
        public required string Status { get; set; }
    }
}
